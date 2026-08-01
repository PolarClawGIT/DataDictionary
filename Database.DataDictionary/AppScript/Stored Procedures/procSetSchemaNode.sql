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
		[ObjectScope]			[AppGeneral].[uddtScopeName] Null,
		[ObjectProperty]		[AppGeneral].[uddtQualifiedName] Null,
		[NodeName]				[AppGeneral].[uddtMember] Null,
		[RenderNodeType]		NVarChar(30) Null,
		[RenderTypeCode]		NVarChar(30) Null,
		[RenderOrder]			Int Not Null,
		Primary Key([NodeId]))

	Insert Into @Values
	Select	X.[NodeId],
			D.[SchemaId],
			NullIf(Trim(D.[ObjectScope]),'') As [ObjectScope],
			NullIf(Trim(D.[ObjectProperty]),'') As [ObjectProperty],
			NullIf(Trim(D.[NodeName]),'') As [NodeName],
			NullIf(Trim(D.[RenderNodeType]),'') As [RenderNodeType],
			NullIf(Trim(D.[RenderTypeCode]),'') As [RenderTypeCode],
			IIF(D.[RenderOrder]<0,0,IsNull(D.[RenderOrder],0)) As [RenderOrder]
	From	@Data D
			Left Join [AppScript].[TemplateModel] M
			On	D.[TemplateId] = M.[TemplateId] And
				@ModelId = M.[ModelId]
			Left Join [AppScript].[SchemaNode] T
			On	D.[SchemaId] = T.[SchemaId] And
				IsNull(NullIf(Trim(D.[ObjectScope]),''),'') = IsNull(T.[ObjectScope],'') And
				IsNull(NullIf(Trim(D.[ObjectProperty]),''),'') = IsNull(T.[ObjectProperty],'')
			Cross Apply (
				Select	Coalesce(T.[NodeId], D.[NodeId], NewId()) As [NodeId]) X
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

	;With [Delta] As (
		Select	[NodeId],
				[SchemaId],
				[ObjectScope],
				[ObjectProperty],
				[NodeName],
				[RenderNodeType],
				[RenderTypeCode],
				[RenderOrder]
		From	@Values
		Except
		Select	[NodeId],
				[SchemaId],
				[ObjectScope],
				[ObjectProperty],
				[NodeName],
				[RenderNodeType],
				[RenderTypeCode],
				[RenderOrder]
		From	[AppScript].[SchemaNode])
	Update [AppScript].[SchemaNode]
	Set		[SchemaId] = S.[SchemaId],
			[ObjectScope] = S.[ObjectScope],
			[ObjectProperty] = S.[ObjectProperty],
			[NodeName] = S.[NodeName],
			[RenderNodeType] = S.[RenderNodeType],
			[RenderTypeCode] = S.[RenderTypeCode],
			[RenderOrder] = S.[RenderOrder]
	From	[AppScript].[SchemaNode] T
			Inner Join [Delta] S
			On	T.[NodeId] = S.[NodeId]
			Inner Join [AppScript].[SchemaDefinition] F
			On	T.[SchemaId] = F.[SchemaId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](F.[TemplateId], 1)
	Print FormatMessage ('Update [AppScript].[SchemaNode]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[SchemaNode] (
			[NodeId],
			[SchemaId],
			[ObjectScope],
			[ObjectProperty],
			[NodeName],
			[RenderNodeType],
			[RenderTypeCode],
			[RenderOrder])
	Select	S.[NodeId],
			S.[SchemaId],
			S.[ObjectScope],
			S.[ObjectProperty],
			S.[NodeName],
			S.[RenderNodeType],
			S.[RenderTypeCode],
			S.[RenderOrder]
	From	@Values S
			Inner Join [AppScript].[SchemaDefinition] F
			On	S.[SchemaId] = F.[SchemaId]
			Left Join [AppScript].[SchemaNode] T
			On	S.[NodeId] = T.[NodeId]
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

