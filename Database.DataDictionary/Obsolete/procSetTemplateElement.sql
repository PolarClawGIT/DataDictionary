CREATE PROCEDURE [AppScript].[procSetTemplateElement]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [AppScript].[udttTemplateElement] ReadOnly
AS
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on TemplateElement.
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
			[ElementId]				UniqueIdentifier NOT NULL,
			[TemplateId]            UniqueIdentifier NOT NULL,
			[ParentElementId]		UniqueIdentifier NULL,
			[ElementName]			[AppGeneral].[uddtQualifiedName] Not Null,
			[RenderOrder]			Int Not Null,
			[RenderValueAs]			NVarChar(20) Not Null,
			[FixedValue]			NVarChar(250) NULL,
			[ObjectScope]			[AppGeneral].[uddtScopeName] Null,
			[ObjectProperty]		[AppGeneral].[uddtQualifiedName] Null,
			[ModelPropertyId]		UniqueIdentifier NULL,
		Primary Key ([ElementId]))

	Insert Into @Values
	Select	IsNull([ElementId], NewId()) As [ElementId],
			IsNull([TemplateId], @TemplateId) As [TemplateId],
			[ParentElementId],
			NullIf(Trim([ElementName]),'') As [ElementName],
			IIF(IsNull([RenderOrder],0) < 0, 0, IsNull([RenderOrder],0)) As [RenderOrder],
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
	Delete From [AppScript].[TemplateNodeOwner_Old]
	From	[AppScript].[TemplateNodeOwner_Old] T
			Left Join @Values S
			On	T.[ElementId] = S.[ElementId]
	Where	S.[ElementId] is Null And
			(@TemplateId is Not Null Or @ModelId is Not Null) And
			(@TemplateId is Null Or @TemplateId = T.[TemplateId])  And
			(@ModelId is Null Or T.[TemplateId] In (
				Select	[TemplateId]
				From	[AppScript].[ScriptingModel]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppScript].[TemplateNodeeOwner]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppScript].[TemplateElement]
	From	[AppScript].[TemplateElement] T
			Left Join @Values S
			On	T.[ElementId] = S.[ElementId]
	Where	S.[ElementId] is Null And
			(@TemplateId is Not Null Or @ModelId is Not Null) And
			(@TemplateId is Null Or @TemplateId = T.[TemplateId])  And
			(@ModelId is Null Or T.[TemplateId] In (
				Select	[TemplateId]
				From	[AppScript].[ScriptingModel]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppScript].[TemplateElement]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));
	
	;With [Delta] As (
		Select	[ElementId],
				[ParentElementId],
				[ElementName],
				[RenderOrder],
				[RenderValueAs],
				[FixedValue],
				[ObjectScope],
				[ObjectProperty],
				[ModelPropertyId]
		From	@Values
		Except
		Select	[ElementId],
				[ParentElementId],
				[ElementName],
				[RenderOrder],
				[RenderValueAs],
				[FixedValue],
				[ObjectScope],
				[ObjectProperty],
				[ModelPropertyId]
		From	[AppScript].[TemplateElement])
	Update	[AppScript].[TemplateElement]
	Set		[ParentElementId] = S.[ParentElementId],
			[ElementName] = S.[ElementName],
			[RenderOrder] = S.[RenderOrder],
			[RenderValueAs] = S.[RenderValueAs],
			[FixedValue] = S.[FixedValue],
			[ObjectScope] = S.[ObjectScope],
			[ObjectProperty] = S.[ObjectProperty],
			[ModelPropertyId] = S.[ModelPropertyId]
	From	[AppScript].[TemplateElement] T
			Inner Join [Delta] S
			On	T.[ElementId] = S.[ElementId]
	Print FormatMessage ('Update [AppScript].[TemplateElement]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[TemplateElement] (
			[ElementId],
			[TemplateId],
			[ParentElementId],
			[ElementName],
			[RenderOrder],
			[RenderValueAs],
			[FixedValue],
			[ObjectScope],
			[ObjectProperty],
			[ModelPropertyId])
	Select	S.[ElementId],
			S.[TemplateId],
			S.[ParentElementId],
			S.[ElementName],
			S.[RenderOrder],
			S.[RenderValueAs],
			S.[FixedValue],
			S.[ObjectScope],
			S.[ObjectProperty],
			S.[ModelPropertyId]
	From	@Values S
			Left Join [AppScript].[TemplateElement] T
			On	S.[ElementId] = T.[ElementId]
	Where	T.[ElementId] is Null
	Print FormatMessage ('Insert [AppScript].[TemplateElement]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
