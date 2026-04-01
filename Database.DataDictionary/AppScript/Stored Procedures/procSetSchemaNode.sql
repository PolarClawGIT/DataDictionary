CREATE PROCEDURE [AppScript].[procSetSchemaNode]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [AppScript].[udttSchemaNode] ReadOnly
AS
-- Transaction Handling
Declare	@TRN_IsNewTran Bit = 0 -- Indicates that the stored procedure started the transaction. Used to handle nested Transactions

Begin Try
	-- Begin Transaction
	If @@TranCount = 0
	  Begin -- Not in a nested/distributed transaction, need to start a transaction
		Begin Transaction
		Select	@TRN_IsNewTran = 1
	  End; -- Begin Transaction

	-- Validation
	If Exists (
		Select	1
		From	@Data D
				Cross Apply [AppSecurity].[funcScriptingAuthorization]([TemplateId], 0))
	Throw 601020, 'Template Not Authorized', 2;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[NodeId]				UniqueIdentifier Not Null,
		[SchemaId]				UniqueIdentifier Not Null,
		[NodeName]				[AppGeneral].[uddtMember] Null,
		[NodeOrder]				Int Not Null,
		[RenderValueAs]			NVarChar(20) Null,
		[FixedValue]			NVarChar(250) Null,
		[ObjectScope]			[AppGeneral].[uddtScopeName] Null,
		[ObjectProperty]		[AppGeneral].[uddtQualifiedName] Null,
		[ModelPropertyId]		UniqueIdentifier NULL,
		Primary Key([NodeId]))

	Insert Into @Values
	Select	X.[NodeId],
			D.[SchemaId],
			NullIf(Trim(D.[NodeName]),'') As [NodeName],
			IIF([NodeOrder]<0,0,IsNull([NodeOrder],0)) As [NodeOrder],
			IsNull(NullIf(Trim(D.[RenderValueAs]),''),'Element') As [RenderValueAs],
			NullIf(Trim(D.[FixedValue]),'') As [FixedValue],
			NullIf(Trim(D.[ObjectScope]),'') As [ObjectScope],
			NullIf(Trim(D.[ObjectProperty]),'') As [ObjectProperty],
			D.[ModelPropertyId]
	From	@Data D
			Left Join [AppScript].[TemplateModel] M
			On	D.[TemplateId] = M.[TemplateId] And
				@ModelId = M.[ModelId]
			Cross Apply (
				Select	Coalesce(D.[NodeId], NewId()) As [NodeId]) X
	Where	(@TemplateId is Null Or @TemplateId = D.[TemplateId]) And
			(@ModelId is Null Or M.[ModelId] is Not Null)
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppScript].[SchemaNode]
	From	[AppScript].[SchemaNode] T
			Inner Join [AppScript].[SchemaDefinition] F
			On	T.[SchemaId] = F.[SchemaId]
			Left Join @Values S
			On	T.[SchemaId] = S.[SchemaId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](F.[TemplateId], 1)
	Where	S.[SchemaId] is Null And
			(@TemplateId is Not Null Or @ModelId is Not Null) And
			(@TemplateId is Null Or @TemplateId = F.[TemplateId])  And
			(@ModelId is Null Or F.[TemplateId] In (
				Select	[TemplateId]
				From	[AppScript].[TemplateModel]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppScript].[SchemaNode]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	-- TODO: Add child table delete


	;With [Delta] As (
		Select	[NodeId],
				[SchemaId],
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
				[SchemaId],
				[NodeName],
				[NodeOrder],
				[RenderValueAs],
				[FixedValue],
				[ObjectScope],
				[ObjectProperty],
				[ModelPropertyId]
		From	[AppScript].[SchemaNode])
	Update [AppScript].[SchemaNode]
	Set		[SchemaId] = S.[SchemaId],
			[NodeName] = S.[NodeName],
			[NodeOrder] = S.[NodeOrder],
			[RenderValueAs] = S.[RenderValueAs],
			[FixedValue] = S.[FixedValue],
			[ObjectScope] = S.[ObjectScope],
			[ObjectProperty] = S.[ObjectProperty],
			[ModelPropertyId] = S.[ModelPropertyId]
	From	[AppScript].[SchemaNode] T
			Inner Join [Delta] S
			On	T.[SchemaId] = S.[SchemaId]
			Inner Join [AppScript].[SchemaDefinition] F
			On	T.[SchemaId] = F.[SchemaId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](F.[TemplateId], 1)
	Print FormatMessage ('Update [AppScript].[SchemaNode]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[SchemaNode] (
			[NodeId],
			[SchemaId],
			[NodeName],
			[NodeOrder],
			[RenderValueAs],
			[FixedValue],
			[ObjectScope],
			[ObjectProperty],
			[ModelPropertyId])
	Select	S.[NodeId],
			S.[SchemaId],
			S.[NodeName],
			S.[NodeOrder],
			S.[RenderValueAs],
			S.[FixedValue],
			S.[ObjectScope],
			S.[ObjectProperty],
			S.[ModelPropertyId]
	From	@Values S
			Inner Join [AppScript].[SchemaDefinition] F
			On	S.[SchemaId] = F.[SchemaId]
			Left Join [AppScript].[SchemaNode] T
			On	S.[SchemaId] = T.[SchemaId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](F.[TemplateId], 1)
	Where	T.[SchemaId] is Null
	Print FormatMessage ('Insert [AppScript].[SchemaNode]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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

