CREATE PROCEDURE [AppScript].[procSetTemplateNode]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [AppScript].[udttTemplateNode] ReadOnly
AS
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on TemplateNode.
*/

-- Transaction Handling
Declare	@TRN_IsNewTran Bit = 0 -- Indicates that the stored procedure started the transaction. Used to handle nested Transactions

Begin Try
	-- Begin Transaction
	If @@TranCount = 0
	  Begin -- Not in a nested/distributed transaction, need to start a transaction
		Begin Transaction
		Select	@TRN_IsNewTran = 1
	  End; -- Begin Transaction

	If Exists (
		Select	1
		From	@Data D
				Cross Apply [AppSecurity].[funcScriptingAuthorization](IsNull([TemplateId],@TemplateId), 0))
	Throw 601020, 'Template Not Authorized', 2;

	-- Clean the Data, helps performance
	Declare @Values Table (
			[NodeId]				UniqueIdentifier NOT NULL,
			[TemplateId]            UniqueIdentifier NOT NULL,
			[NodeName]				[AppGeneral].[uddtNameSpaceMember] Not Null,
			[NodeOrder]				Int Not Null,
			[RenderValueAs]			NVarChar(20) Not Null,
			[FixedValue]			NVarChar(250) NULL,
			[ObjectScope]			[AppGeneral].[uddtScopeName] Null,
			[ObjectProperty]		[AppGeneral].[uddtQualifiedName] Null,
			[ModelPropertyId]		UniqueIdentifier NULL,
		Primary Key ([NodeId]))

	Insert Into @Values
	Select	IsNull([NodeId], NewId()) As [NodeId],
			IsNull([TemplateId], @TemplateId) As [TemplateId],
			NullIf(Trim([NodeName]),'') As [NodeName],
			IIF(IsNull([NodeOrder],0) < 0, 0, IsNull([NodeOrder],0)) As [NodeOrder],
			IsNull([RenderValueAs],'Text') As [RenderValueAs],
			NullIf(Trim([FixedValue]),'') As [FixedValue],
			NullIf(Trim([ObjectScope]),'') As [ObjectScope],
			NullIf(Trim([ObjectProperty]),'') As [ObjectProperty],
			[ModelPropertyId]
	From	@Data D
	Where	(@TemplateId is Null And [TemplateId] is Not Null) Or
			(@TemplateId is Not Null And IsNull([TemplateId], @TemplateId) = @TemplateId)
	Print FormatMessage ('Insert @Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
;	With [Delete] As (
		Select	P.[NodeId],
				P.[NodeOwnerId],
				P.[TemplateId]
		From	[AppScript].[TemplateNode] T
				Inner Join [AppScript].[TemplateNodeOwner] P
				On	T.[NodeId] = P.[NodeId] And
					T.[TemplateId] = P.[TemplateId]
				Left Join @Values S
				On	T.[NodeId] = S.[NodeId] And
					T.[TemplateId] = S.[TemplateId]
		Where	S.[NodeId] is Null And
				(@TemplateId is Not Null Or @ModelId is Not Null) And
				(@TemplateId is Null Or @TemplateId = T.[TemplateId])  And
				(@ModelId is Null Or T.[TemplateId] In (
					Select	[TemplateId]
					From	[AppScript].[ScriptingModel]
					Where	[ModelId] = @ModelId))
		Union All
		Select	P.[NodeId],
				P.[NodeOwnerId],
				P.[TemplateId]
		From	[Delete] D
				Inner Join [AppScript].[TemplateNodeOwner] P
				On	D.[NodeId] = P.[NodeOwnerId] and 
					D.[TemplateId] = P.[TemplateId])
	Delete From [AppScript].[TemplateNodeOwner]
	From	[AppScript].[TemplateNodeOwner] T
			Inner Join [Delete] D
			On	T.[NodeId] = D.[NodeId] And
				T.[NodeOwnerId] = D.[NodeOwnerId] And
				T.[TemplateId] = D.[TemplateId]
	Print FormatMessage ('Delete [AppScript].[TemplateParentNode]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppScript].[TemplateNode]
	From	[AppScript].[TemplateNode] T
			Left Join @Values S
			On	T.[NodeId] = S.[NodeId]
	Where	S.[NodeId] is Null And
			(@TemplateId is Not Null Or @ModelId is Not Null) And
			(@TemplateId is Null Or @TemplateId = T.[TemplateId])  And
			(@ModelId is Null Or T.[TemplateId] In (
				Select	[TemplateId]
				From	[AppScript].[ScriptingModel]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppScript].[TemplateNode]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));
	
	;With [Delta] As (
		Select	[NodeId],
				[NodeName],
				[NodeOrder],
				[RenderValueAs],
				[FixedValue],
				[ObjectScope],
				[ObjectProperty],
				[ModelPropertyId]
		From	@Values
		Except
		Select	[NodeId],
				[NodeName],
				[NodeOrder],
				[RenderValueAs],
				[FixedValue],
				[ObjectScope],
				[ObjectProperty],
				[ModelPropertyId]
		From	[AppScript].[TemplateNode])
	Update	[AppScript].[TemplateNode]
	Set		[NodeName] = S.[NodeName],
			[NodeOrder] = S.[NodeOrder],
			[RenderValueAs] = S.[RenderValueAs],
			[FixedValue] = S.[FixedValue],
			[ObjectScope] = S.[ObjectScope],
			[ObjectProperty] = S.[ObjectProperty],
			[ModelPropertyId] = S.[ModelPropertyId]
	From	[AppScript].[TemplateNode] T
			Inner Join [Delta] S
			On	T.[NodeId] = S.[NodeId]
	Print FormatMessage ('Update [AppScript].[TemplateNode]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[TemplateNode] (
			[NodeId],
			[TemplateId],
			[NodeName],
			[NodeOrder],
			[RenderValueAs],
			[FixedValue],
			[ObjectScope],
			[ObjectProperty],
			[ModelPropertyId])
	Select	S.[NodeId],
			S.[TemplateId],
			S.[NodeName],
			S.[NodeOrder],
			S.[RenderValueAs],
			S.[FixedValue],
			S.[ObjectScope],
			S.[ObjectProperty],
			S.[ModelPropertyId]
	From	@Values S
			Left Join [AppScript].[TemplateNode] T
			On	S.[NodeId] = T.[NodeId]
	Where	T.[NodeId] is Null
	Print FormatMessage ('Insert [AppScript].[TemplateNode]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Commit Transaction
	If @TRN_IsNewTran = 1
	  Begin -- If this is the outer transaction, commit it
		If XAct_State() = -1 Throw 103930, 'The current transaction cannot be committed and cannot support operations that write to the log file. Roll back the transaction. (Msg- 3930)', 100
		Commit Transaction
		Print FormatMessage ('Commit Transaction Issued ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
	  End -- Commit Transaction
	  -- This is a nested transaction, must be committed by outer transaction
	Else Print FormatMessage ('Commit Transaction Pending ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
End Try
Begin Catch
	-- Debug Data
	Print FormatMessage ('*** Error Report: %s ***', Object_Name(@@ProcID))
	Print FormatMessage (' Message- %s', ERROR_MESSAGE())
	Print FormatMessage (' Number- %i', ERROR_NUMBER())
	Print FormatMessage (' Severity- %i', ERROR_SEVERITY())
	Print FormatMessage (' State- %i', ERROR_STATE())
	Print FormatMessage (' Procedure- %s', ERROR_PROCEDURE())
	Print FormatMessage (' Line- %i', ERROR_LINE())
	Print FormatMessage (' @@TranCount - %i', @@TranCount)
	Print FormatMessage (' @@NestLevel - %i', @@NestLevel)
	Print FormatMessage (' Original_Login - %s', Original_Login())
	Print FormatMessage (' Current_User - %s', Current_User)
	Print FormatMessage (' XAct_State - %i', XAct_State())
	Print '*** Debug Report ***'

	Print FormatMessage ('*** End Report: %s ***', Object_Name(@@ProcID))

	-- Rollback Transaction
	If @TRN_IsNewTran = 1
	  Begin -- If this is the outer transaction, roll it back
		Rollback Transaction
		Print FormatMessage ('Rollback Transaction Issued ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
	  End -- Rollback Transaction
	-- This is a nested transaction, must be rolled back by outer transaction
	Else Print FormatMessage ('Rollback Transaction Pending ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))

	If ERROR_SEVERITY() Not In (0, 11) Throw -- Re-throw the Error
End Catch
GO
