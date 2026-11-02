CREATE PROCEDURE [AppScript].[procSetTemplateDocument]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [AppScript].[udttTemplateDocument] ReadOnly
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
		[DocumentId]		UniqueIdentifier Not Null,
		[TemplateId]		UniqueIdentifier Not Null,
		[DocumentTitle]		[AppGeneral].[uddtTitle] Not Null,
		[SchemaId]			UniqueIdentifier Null,
		[ObjectId]			UniqueIdentifier Null,
		[DataFileName]		[AppGeneral].[uddtFileName] Null,
		[TransformId]		UniqueIdentifier Null,
		[TransformFileName]	[AppGeneral].[uddtFileName] Null,
		[ScriptedFileName]	[AppGeneral].[uddtFileName] Null,
		Primary Key([DocumentId]))

	-- Handles Objects
	Declare @Objects [AppScript].[udttTemplateObject]

	;With [Object] As (
		Select	[TemplateId],
				[ObjectId],
				[ObjectScope],
				[AppScript].[funcObjectPath] ([ObjectId]) As [ObjectPath],
				Convert(Int, NullIf([IsExcluded],0)) As [IsExcluded],
				Convert(Int, NullIf([KeepOrphaned],0)) As [KeepOrphaned]
		From	[AppScript].[TemplateObject]
		Where	[ObjectScope] is Not Null And
				[TemplateId] In (
					Select	[TemplateId]
					From	@Data
					Union	
					Select	@TemplateId
					Where	@TemplateId is Not Null) And
				([IsExcluded] = 1 Or [KeepOrphaned] = 1)),
	[Data] As (
		Select	IsNull(D.[TemplateId], @TemplateId) As [TemplateId],
				D.[ObjectScope],
				P.[QualifiedName] As [ObjectPath],
				Convert(Int, IsNull(D.[IsExcluded],0)) As [IsExcluded],
				Convert(Int, IsNull(D.[KeepOrphaned],0)) As [KeepOrphaned]
		From	@Data D
				Cross Apply [AppGeneral].[funcParseName](D.[ObjectPath]) P
		Where	P.[IsBase] = 1 And
				D.[ObjectPath] is Not Null And
				D.[ObjectScope] is Not Null),
	[Combine] As (
		Select	[TemplateId],
				[ObjectScope],
				[ObjectPath]
		From	[Object]
		Union	
		Select	[TemplateId],
				[ObjectScope],
				[ObjectPath]
		From	[Data] D)
	Insert Into @Objects (
		[TemplateId],
		[ObjectId],
		[ObjectScope],
		[ObjectPath],
		[IsExcluded],
		[KeepOrphaned])
	Select	C.[TemplateId],
			IsNull(O.[ObjectId], NewId()) As [ObjectId],
			C.[ObjectScope],
			C.[ObjectPath],
			Max(Coalesce(D.[IsExcluded], O.[IsExcluded], 0)) As [IsExcluded],
			Max(Coalesce(D.[KeepOrphaned], O.[KeepOrphaned], 0)) As [KeepOrphaned]
	From	[Combine] C
			Left Join [Data] D
			On	C.[TemplateId] = D.[TemplateId] And
				C.[ObjectScope] = D.[ObjectScope] And
				C.[ObjectPath] = D.[ObjectPath]
			Left Join [Object] O
			On	C.[TemplateId] = O.[TemplateId] And
				C.[ObjectScope] = O.[ObjectScope] And
				C.[ObjectPath] = O.[ObjectPath]
	Group By C.[TemplateId],
			O.[ObjectId],
			C.[ObjectScope],
			C.[ObjectPath]
	Print FormatMessage ('@Objects: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Build Values
	Insert Into @Values
	Select	X.[DocumentId],
			D.[TemplateId],
			NullIf(Trim(D.[DocumentTitle]),'') As [DocumentTitle],
			D.[SchemaId],
			O.[ObjectId],
			NullIf(Trim(D.[DataFileName]),'') As [DataFileName],
			D.[TransformId],
			NullIf(Trim(D.[TransformFileName]),'') As [TransformFileName],
			NullIf(Trim(D.[ScriptedFileName]),'') As [ScriptedFileName]
	From	@Data D
			Left Join [AppScript].[TemplateModel] M
			On	D.[TemplateId] = M.[TemplateId] And
				@ModelId = M.[ModelId]
			Cross Apply  [AppGeneral].[funcParseName](D.[ObjectPath]) P
			Left Join @Objects O
			On	IsNull(D.[TemplateId], @TemplateId)  = O.[TemplateId] And
				D.[ObjectScope] = O.[ObjectScope] And
				P.[QualifiedName] = O.[ObjectPath]
			Cross Apply (
				Select	Coalesce(D.[DocumentId], NewId()) As [DocumentId]) X
	Where	(@TemplateId is Null Or @TemplateId = D.[TemplateId]) And
			(@ModelId is Null Or M.[ModelId] is Not Null)
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Exec [AppScript].[procSetTemplateObject] @ModelId = @ModelId, @TemplateId = @TemplateId, @Data = @Objects

	Delete From [AppScript].[TemplateDocument]
	From	[AppScript].[TemplateDocument] T
			Inner Join [AppScript].[Transform] P
				On	T.[TransformId] = P.[TransformId] 
			Left Join @Values S
			On	T.[DocumentId] = S.[DocumentId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](P.[TemplateId], 1)
	Where	S.[DocumentId] is Null And
			(@TemplateId is Not Null Or @ModelId is Not Null) And
			(@TemplateId is Null Or @TemplateId = P.[TemplateId])  And
			(@ModelId is Null Or P.[TemplateId] In (
				Select	[TemplateId]
				From	[AppScript].[TemplateModel]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppScript].[TemplateDocument]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[DocumentId],
				[TemplateId],
				[DocumentTitle],
				[SchemaId],
				[ObjectId],
				[DataFileName],
				[TransformId],
				[TransformFileName],
				[ScriptedFileName]
		From	@Values
		Except
		Select	[DocumentId],
				[TemplateId],
				[DocumentTitle],
				[SchemaId],
				[ObjectId],
				[DataFileName],
				[TransformId],
				[TransformFileName],
				[ScriptedFileName]
		From	[AppScript].[TemplateDocument] D)
	Update [AppScript].[TemplateDocument]
	Set		--[DocumentId] = S.[DocumentId],
			--[TemplateId] = S.[TemplateId],
			[DocumentTitle] = S.[DocumentTitle],
			[SchemaId] = S.[SchemaId],
			[ObjectId] = S.[ObjectId],
			[DataFileName] = S.[DataFileName],
			[TransformId] = S.[TransformId],
			[TransformFileName] = S.[TransformFileName],
			[ScriptedFileName] = S.[ScriptedFileName]
	From	[AppScript].[TemplateDocument] T
			Inner Join [Delta] S
			On	T.[DocumentId] = S.[DocumentId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[TemplateId], 1)
	Print FormatMessage ('Update [AppScript].[TemplateDocument]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[TemplateDocument] (
			[DocumentId],
			[TemplateId],
			[DocumentTitle],
			[SchemaId],
			[ObjectId],
			[DataFileName],
			[TransformId],
			[TransformFileName],
			[ScriptedFileName])
	Select	S.[DocumentId],
			S.[TemplateId],
			S.[DocumentTitle],
			S.[SchemaId],
			S.[ObjectId],
			S.[DataFileName],
			S.[TransformId],
			S.[TransformFileName],
			S.[ScriptedFileName]
	From	@Values S
			Left Join [AppScript].[TemplateDocument] T
			On	S.[DocumentId] = T.[DocumentId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[TemplateId], 1)
	Where	T.[DocumentId] is Null
	Print FormatMessage ('Insert [AppScript].[Document]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
