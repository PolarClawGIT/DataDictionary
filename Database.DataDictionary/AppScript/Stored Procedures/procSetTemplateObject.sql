CREATE PROCEDURE [AppScript].[procSetTemplateObject]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [AppScript].[udttTemplateObject] ReadOnly
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
		[ObjectId]			UniqueIdentifier Not NULL,
		[TemplateId]		UniqueIdentifier Not NULL,
		[ObjectScope]		[AppGeneral].[uddtScopeName] Null,
		[ObjectName]		[AppGeneral].[uddtPath] Not Null,
		[ObjectPath]		[AppGeneral].[uddtPath] Null,
		[ObjectMember]		[AppGeneral].[uddtMember] Not Null,
		[IsExcluded]		Bit Not Null,
		[KeepOrphaned]		Bit Not Null,
		Primary Key([ObjectId]))

;	With [Data] As (
		Select	IIF(N.[IsBase] =1, D.[ObjectId], Null) As [ObjectId],
				IsNull(D.[TemplateId], @TemplateId) As [TemplateId],
				IIF(N.[IsBase] =1, D.[ObjectScope], Null) As [ObjectScope],
				N.[QualifiedName] As [ObjectName],
				N.[ParentName] As [ObjectPath],
				N.[MemberName] As [ObjectMember],
				IIF(N.[IsBase] =1, D.[IsExcluded], Null) As [IsExcluded],
				IIF(N.[IsBase] =1, D.[KeepOrphaned], Null) As [KeepOrphaned]
		From	@Data D
				Cross Apply [AppGeneral].[funcParseName](D.[ObjectName]) N),
	[Group] As (
		Select	X.[ObjectId],
				IsNull(D.[TemplateId], @TemplateId) As [TemplateId],
				D.[ObjectScope],
				D.[ObjectName],
				D.[ObjectPath],
				D.[ObjectMember],
				Convert(Bit,Max(Convert(TinyInt, IsNull(D.[IsExcluded],0))) Over (
					Partition By D.[TemplateId], D.[ObjectName])) As [IsExcluded],
				Convert(Bit,Max(Convert(TinyInt, IsNull(D.[KeepOrphaned],0))) Over (
					Partition By D.[TemplateId], D.[ObjectName])) As [KeepOrphaned],
				Dense_Rank() Over (
					Partition By D.[TemplateId], D.[ObjectName]
					Order By 
						Case When D.[ObjectScope] is Not Null Then 0 Else 1 End,
						Case When H.[ObjectId] is Not Null Then 0 Else 1 End,
						X.[ObjectId]
					) As [RankIndex]
		From	[Data] D
				Left Join [AppScript].[TemplateObjectHS] H
				On	IsNull(D.[TemplateId], @TemplateId) = H.[TemplateId] And
					IsNull(D.[ObjectPath],'') = IsNull(H.[ObjectPath],'') And
					D.[ObjectMember] = H.[ObjectMember]
				Cross Apply (Select Coalesce(H.[ObjectId], D.[ObjectId], NewID()) As [ObjectId]) X)
	Insert Into @Values
	Select	[ObjectId],
			[TemplateId],
			[ObjectScope],
			[ObjectName],
			[ObjectPath],
			[ObjectMember],
			[IsExcluded],
			[KeepOrphaned]
	From	[Group] D
	Where	[RankIndex] = 1

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppScript].[TemplateObject]
	From	[AppScript].[TemplateObject] T
			Left Join @Values S
			On	T.[ObjectId] = S.[ObjectId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[TemplateId], 1)
	Where	S.[ObjectId] is Null And
			(@TemplateId is Not Null Or @ModelId is Not Null) And
			(@TemplateId is Null Or @TemplateId = T.[TemplateId])  And
			(@ModelId is Null Or T.[TemplateId] In (
				Select	[TemplateId]
				From	[AppScript].[TemplateModel]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppScript].[DocumentObject]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppScript].[Document]
	From	[AppScript].[Document] T
			Left Join @Values S
			On	T.[ObjectId] = S.[ObjectId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[TemplateId], 1)
	Where	S.[ObjectId] is Null And
			(@TemplateId is Not Null Or @ModelId is Not Null) And
			(@TemplateId is Null Or @TemplateId = T.[TemplateId])  And
			(@ModelId is Null Or T.[TemplateId] In (
				Select	[TemplateId]
				From	[AppScript].[TemplateModel]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppScript].[Document]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	C.[ObjectId],
				C.[TemplateId],
				P.[ObjectId] As [ParentObjectId],
				C.[ObjectScope],
				C.[ObjectMember],
				C.[IsExcluded],
				C.[KeepOrphaned]
		From	@Values C
				Left Join @Values P
				On	C.[TemplateId] = P.[TemplateId] And
					C.[ObjectPath] = P.[ObjectName]
		Except
		Select	[ObjectId],
				[TemplateId],
				[ParentObjectId],
				[ObjectScope],
				[ObjectMember],
				[IsExcluded],
				[KeepOrphaned]
		From	[AppScript].[TemplateObject])
	Update [AppScript].[TemplateObject]
	Set		--[TemplateId] = S.[TemplateId],
			[ParentObjectId] = S.[ParentObjectId],
			[ObjectScope] = S.[ObjectScope],
			[ObjectMember] = S.[ObjectMember],
			[IsExcluded] = S.[IsExcluded],
			[KeepOrphaned] = S.[KeepOrphaned]
	From	[AppScript].[TemplateObject] T
			Inner Join [Delta] S
			On	T.[ObjectId] = S.[ObjectId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[TemplateId], 1)
	Print FormatMessage ('Update [AppScript].[DocumentObject]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[TemplateObject] (
			[ObjectId],
			[TemplateId],
			[ParentObjectId],
			[ObjectScope],
			[ObjectMember],
			[IsExcluded],
			[KeepOrphaned])
	Select	S.[ObjectId],
			S.[TemplateId],
			P.[ObjectId] As [ParentObjectId],
			S.[ObjectScope],
			S.[ObjectMember],
			S.[IsExcluded],
			S.[KeepOrphaned]
	From	@Values S
				Left Join @Values P
				On	S.[TemplateId] = P.[TemplateId] And
					S.[ObjectPath] = P.[ObjectName]
			Left Join [AppScript].[TemplateObject] T
			On	S.[ObjectId] = T.[ObjectId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[TemplateId], 1)
	Where	T.[ObjectId] is Null
	Print FormatMessage ('Insert [AppScript].[DocumentObject]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
/*
Begin Try;
	Begin Transaction;
	Set NoCount On;

	-- Setup
	Declare @ModelId UniqueIdentifier = (Select [ModelId] From [AppModel].[Model] Where [ModelTitle] = 'Unit Test')

	Declare	@TemplateId UniqueIdentifier = NewId(),
			@Template [AppScript].[udttTemplate]

	Insert Into @Template ([TemplateId], [TemplateTitle], [TemplateDescription])
	Values (@TemplateId, 'Unit Test', 'Testing')
	Exec [AppScript].[procSetTemplate] @ModelId = @ModelId, @TemplateId = @TemplateId, @Data = @Template


	Declare @Object [AppScript].[udttDocumentObject]

	-- Validate [udttDocumentObject]
	Insert Into @Object
	Exec [AppScript].[procGetDocumentObject]

	Insert Into @Object ([TemplateId], [ObjectScope], [ObjectName], [KeepOrphaned])
	Values	(@TemplateId, 'Model.Attribute', '[Root].[Subject01].Item01', 0),
			(@TemplateId, 'Model.Attribute', '[Root].[Subject01].Item02', 0),
			(@TemplateId, 'Model.Attribute', '[Root].[Subject02].Item01', 0),
			(@TemplateId, 'Model.Attribute', '[Root].[Subject02].Item03', 0),
			(@TemplateId, 'Model.Attribute', '[Root].[Subject02].Duplicate', 0),
			(@TemplateId, 'Model.Attribute', '[Root].[Subject02].Duplicate', 0),
			(@TemplateId, 'Model.Attribute', '[Root].[Subject02].Orphaned', 1),
			(@TemplateId, 'Model.Attribute', 'Last', 0)

	Select	'@Object', *
	From	@Object

	Exec [AppScript].[procSetDocumentObject] @ModelId = @ModelId, @TemplateId = @TemplateId, @Data = @Object


	Exec [AppScript].[procGetDocumentObject]

	Select	'[DocumentObjectTree]', *
	From	[AppScript].[DocumentObjectTree]

	-- By default, throw and error and exit without committing
;	Throw 50000, 'Abort process, comment out this line when ready to actual Commit the transaction',255;
	
	Commit Transaction;
	Print 'Commit Issued';
End Try
Begin Catch
	Print FormatMessage ('*** Error Report: %s ***', Object_Name(@@ProcID));
	Print FormatMessage (' Message- %s', ERROR_MESSAGE());
	Print FormatMessage (' Number- %i', ERROR_NUMBER());
	Print FormatMessage (' Severity- %i', ERROR_SEVERITY());
	Print FormatMessage (' State- %i', ERROR_STATE());
	Print FormatMessage (' Procedure- %s', ERROR_PROCEDURE());
	Print FormatMessage (' Line- %i', ERROR_LINE());
	Print FormatMessage (' @@TranCount - %i', @@TranCount);
	Print FormatMessage (' @@NestLevel - %i', @@NestLevel);
	Print FormatMessage (' Original_Login - %s', Original_Login());
	Print FormatMessage (' Current_User - %s', Current_User);
	Print FormatMessage (' XAct_State - %i', XAct_State());
	Print '--- Debug Data ---';

	-- Rollback Transaction
	Print 'Rollback Issued';
	Rollback Transaction;
	If(ERROR_NUMBER()<> 50000) Throw;
End Catch;
*/