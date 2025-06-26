CREATE PROCEDURE [AppModel].[procSetModel]
		@ModelId UniqueIdentifier = Null,
		@Data [AppModel].[udttModel] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ApplicationModel.
*/

-- Transaction Handling
Declare	@TRN_IsNewTran Bit = 0, -- Indicates that the stored procedure started the transaction. Used to handle nested Transactions
		@RowCount Int = 0 -- @@RowCount is reset just by reading @@RowCount. This is used to persist the value.

Begin Try
	-- Begin Transaction
	If @@TranCount = 0
	  Begin -- Not in a nested/distributed transaction, need to start a transaction
		Begin Transaction
		Select	@TRN_IsNewTran = 1
	  End; -- Begin Transaction

	-- Validation
	If @ModelId is Not Null And
		Exists (
			Select	1
			From	@Data
			Where	IsNull([ModelId], @ModelId) <> @ModelId)
	Throw 601010, '@Data contains other model then @ModelId', 1;

	If Exists (
		Select	1
		From	@Data
				Cross Apply [AppSecurity].[funcModelAuthorization](IsNull([ModelId], @ModelId), 0)) 
	Throw 601020, 'Model Not Authorized', 2;

	-- Clean the Data
	Declare @Values Table (
		[ModelId] UniqueIdentifier NOT NULL,
		[ModelTitle] [AppGeneral].[uddtTitle] Not Null,
		[ModelDescription] [AppGeneral].[uddtDescription] Null,
		Primary Key ([ModelId]),
		Unique ([ModelTitle]))

	Insert Into @Values
	Select	Coalesce(D.[ModelId], @ModelId, NewId()),
			NullIf(Trim([ModelTitle]),'') As [ModelTitle],
			NullIf(Trim([ModelDescription]),'') As [ModelDescription]
	From	@Data D
	Where	(@ModelId is Null Or @ModelId = Coalesce(D.[ModelId], @ModelId))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Deal with Ownership, Sets up Row Level Security
	Insert Into [AppSecurity].[SecurableOwner] (
			[PrincipalId],
			[SecurableId])
	Select	S.[PrincipalId],
			V.[ModelId]
	From	@Values V
			Cross Apply [AppSecurity].[funcModelAuthorization](V.[ModelId],1) S
	Where	S.[IsModelOwner] = 1 And
			S.[IsModelAdmin] = 0 And
			S.[HasOwner] = 0 And
			S.[PrincipalId] is not null
	Set @RowCount = @@RowCount
	If @RowCount > 0 Print FormatMessage ('Insert [AppSecurity].[SecurityOwner]: %i, %s', @RowCount, Convert(VarChar,GetDate()));

	-- Apply Changes
	Delete From [AppModel].[ModelAttribute]
	From	[AppModel].[ModelAttribute] T
			Left Join @Values S
			On	T.[ModelId] = S.[ModelId]
			Cross Apply [AppSecurity].[funcModelAuthorization](T.[ModelId], 1) 
	Where	S.[ModelId] is Null And
			T.[ModelId] = @ModelId
	Set @RowCount = @@RowCount
	If @RowCount > 0 Print FormatMessage ('Delete [AppModel].[ModelAttribute] (Model): %i, %s',@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[ModelEntity]
	From	[AppModel].[ModelEntity] T
			Left Join @Values S
			On	T.[ModelId] = S.[ModelId]
			Cross Apply [AppSecurity].[funcModelAuthorization](T.[ModelId], 1) 
	Where	S.[ModelId] is Null And
			T.[ModelId] = @ModelId
	Set @RowCount = @@RowCount
	If @RowCount > 0 Print FormatMessage ('Delete [AppModel].[ModelEntity] (Model): %i, %s',@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[ModelProperty]
	From	[AppModel].[ModelProperty] T
			Left Join @Values S
			On	T.[ModelId] = S.[ModelId]
			Cross Apply [AppSecurity].[funcModelAuthorization](T.[ModelId], 1) 
	Where	S.[ModelId] is Null And
			T.[ModelId] = @ModelId
	Set @RowCount = @@RowCount
	If @RowCount > 0 Print FormatMessage ('Delete [AppModel].[ModelProperty] (Model): %i, %s',@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[ModelDefinition]
	From	[AppModel].[ModelDefinition] T
			Left Join @Values S
			On	T.[ModelId] = S.[ModelId]
			Cross Apply [AppSecurity].[funcModelAuthorization](T.[ModelId], 1) 
	Where	S.[ModelId] is Null And
			T.[ModelId] = @ModelId
	Set @RowCount = @@RowCount
	If @RowCount > 0 Print FormatMessage ('Delete [AppModel].[ModelDefinition] (Model): %i, %s',@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[ModelProcess]
	From	[AppModel].[ModelProcess] T
			Left Join @Values S
			On	T.[ModelId] = S.[ModelId]
			Cross Apply [AppSecurity].[funcModelAuthorization](T.[ModelId], 1) 
	Where	S.[ModelId] is Null And
			T.[ModelId] = @ModelId
	Set @RowCount = @@RowCount
	If @RowCount > 0 Print FormatMessage ('Delete [AppModel].[ModelProcess] (Model): %i, %s',@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[SubjectArea]
	From	[AppModel].[SubjectArea] T
			Left Join @Values S
			On	T.[ModelId] = S.[ModelId]
			Cross Apply [AppSecurity].[funcModelAuthorization](T.[ModelId], 1) 
	Where	S.[ModelId] is Null And
			T.[ModelId] = @ModelId
	Set @RowCount = @@RowCount
	If @RowCount > 0 Print FormatMessage ('Delete [AppModel].[ModelSubjectArea] (Model): %i, %s',@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppLibrary].[LibraryModel]
	From	[AppLibrary].[LibraryModel] T
			Left Join @Values S
			On	T.[ModelId] = S.[ModelId]
			Cross Apply [AppSecurity].[funcModelAuthorization](T.[ModelId], 1) 
	Where	S.[ModelId] is Null And
			T.[ModelId] = @ModelId
	Set @RowCount = @@RowCount
	If @RowCount > 0 Print FormatMessage ('Delete [AppLibrary].[LibraryModel] (Model): %i, %s',@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppCatalog].[CatalogModel]
	From	[AppCatalog].[CatalogModel] T
			Left Join @Values S
			On	T.[ModelId] = S.[ModelId]
			Cross Apply [AppSecurity].[funcModelAuthorization](T.[ModelId], 1) 
	Where	S.[ModelId] is Null  And
			T.[ModelId] = @ModelId
	Set @RowCount = @@RowCount
	IF @RowCount > 0 Print FormatMessage ('Delete [AppCatalog].[CatalogModel] (Model): %i, %s', @RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[Model]
	From	[AppModel].[Model] T
			Left Join @Values S
			On	T.[ModelId] = S.[ModelId]
			Cross Apply [AppSecurity].[funcModelAuthorization](T.[ModelId], 1) 
	Where	S.[ModelId] is Null And
			T.[ModelId] = @ModelId -- @ModelId must be specified
	Print FormatMessage ('Delete [AppModel].[Model]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[ModelId],
				[ModelTitle],
				[ModelDescription]
		From	@Values
		Except
		Select	[ModelId],
				[ModelTitle],
				[ModelDescription]
		From	[AppModel].[Model])
	Update [AppModel].[Model]
	Set		[ModelTitle] = S.[ModelTitle],
			[ModelDescription] = S.[ModelDescription]
	From	[AppModel].[Model] T
			Inner Join [Delta] S
			On	T.[ModelId] = S.[ModelId]
			Cross Apply [AppSecurity].[funcModelAuthorization](T.[ModelId], 1) 
	Print FormatMessage ('Update [AppModel].[Model]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[Model] (
			[ModelId],
			[ModelTitle],
			[ModelDescription])
	Select	S.[ModelId],
			S.[ModelTitle],
			S.[ModelDescription]
	From	@Values S
			Left Join [AppModel].[Model] T
			On	S.[ModelId] = T.[ModelId]
			Cross Apply [AppSecurity].[funcModelAuthorization](S.[ModelId], 1) 
	Where	T.[ModelId] is Null
	Print FormatMessage ('Insert [AppModel].[Model]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
	-- Rollback Transaction
	If @TRN_IsNewTran = 1
	  Begin -- If this is the outer transaction, roll it back
		Rollback Transaction
		Print FormatMessage ('Rollback Transaction Issued ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
	  End -- Rollback Transaction
	-- This is a nested transaction, must be rolled back by outer transaction
	Else Print FormatMessage ('Rollback Transaction Pending ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))

	If ERROR_NUMBER() >= 50000 Exec [AppGeneral].[procThrowHelpSubject]
	Else If ERROR_SEVERITY() Not In (0, 11) Throw;
End Catch
GO
