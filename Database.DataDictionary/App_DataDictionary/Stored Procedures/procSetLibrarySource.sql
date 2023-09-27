CREATE PROCEDURE [App_DataDictionary].[procSetLibrarySource]
		@ModelId UniqueIdentifier,
		@Data [App_DataDictionary].[typeLibrarySource] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on LibrarySource.
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

	-- Clean the Data
	Declare @Values [App_DataDictionary].[typeLibrarySource]
	Insert Into @Values
	Select	Coalesce(P.[LibraryId], D.[LibraryId], NewId()) As [LibraryId],
			NullIf(Trim(D.[LibraryTitle]),'') As [LibraryTitle],
			NullIf(Trim(D.[LibraryDescription]),'') As [LibraryDescription],
			NullIf(Trim(D.[AssemblyName]),'') As [AssemblyName],
			NullIf(Trim(D.[SourceFile]),'') As [SourceFile],
			IsNull(D.[SourceDate],GetDate()) As [SourceDate],
			D.[SysStart]
	From	@Data D
			Left Join [App_DataDictionary].[ModelLibrary] C
			On	C.[ModelId] = @ModelId
			Left Join [App_DataDictionary].[LibrarySource] P
			On	C.[LibraryId] = P.[LibraryId] And
				D.[AssemblyName] = P.[AssemblyName]

	-- Validation
	If Not Exists (Select 1 From [App_DataDictionary].[Model] Where [ModelId] = @ModelId)
	Throw 50000, '[ModelId] could not be found that matched the parameter', 1;

	If Exists (
		Select	[AssemblyName]
		From	@Values
		Group By [AssemblyName]
		Having	Count(*) > 1)
	Throw 50000, '[AssemblyName] cannot be duplicate', 2;

	If Exists ( -- Set [SysStart] to Null in parameter data to bypass this check
		Select	D.[LibraryId]
		From	@Values D
				Inner Join [App_DataDictionary].[DatabaseCatalog] A
				On D.[LibraryId] = A.[CatalogId]
		Where	IsNull(D.[SysStart],A.[SysStart]) <> A.[SysStart])
	Throw 50000, '[SysStart] indicates that the Database Row may have changed since the source Row was originally extracted', 3;

	-- Cascade Delete
	Declare @Delete [App_DataDictionary].[typeLibrarySource] 

	Insert Into @Delete ([LibraryId])
	Select	T.[LibraryId]
	From	[App_DataDictionary].[ModelLibrary] T
			Left Join @Values V
			On	V.[LibraryId] = T.[LibraryId]
	Where	V.[LibraryId] is Null And
			T.[ModelId] = @ModelId

	Delete From [App_DataDictionary].[LibraryMember]
	Where	[LibraryId] In (Select [LibraryId] From @Delete)

	Delete From [App_DataDictionary].[LibraryNameSpace]
	Where	[LibraryId] In (Select [LibraryId] From @Delete)

	-- Apply Changes
	;With [Delta] As (
		Select	[LibraryId],
				[LibraryTitle],
				[LibraryDescription],
				[AssemblyName],
				[SourceFile],
				[SourceDate]
		From	@Values
		Except
		Select	[LibraryId],
				[LibraryTitle],
				[LibraryDescription],
				[AssemblyName],
				[SourceFile],
				[SourceDate]
		From	[App_DataDictionary].[LibrarySource]),
	[Data] As (
		Select	V.[LibraryId],
				V.[LibraryTitle],
				V.[LibraryDescription],
				V.[AssemblyName],
				V.[SourceFile],
				V.[SourceDate],
				IIF(D.[LibraryId] is Null,1, 0) As [IsDiffrent]
		From	@Values V
				Left Join [Delta] D
				On	V.[LibraryId] = D.[LibraryId])
	Merge [App_DataDictionary].[LibrarySource] As T
	Using [Data] As S
	On	T.[LibraryId] = S.[LibraryId]
	When Matched And S.[IsDiffrent] = 1 Then Update
		Set	[LibraryTitle] = S.[LibraryTitle],
			[LibraryDescription] = S.[LibraryDescription],
			[AssemblyName] = S.[AssemblyName],
			[SourceFile] = S.[SourceFile],
			[SourceDate] = S.[SourceDate]
	When Not Matched by Target Then
		Insert ([LibraryId], [LibraryTitle], [LibraryDescription], [AssemblyName], [SourceFile], [SourceDate])
		Values ([LibraryId], [LibraryTitle], [LibraryDescription], [AssemblyName], [SourceFile], [SourceDate])
	When Not Matched by Source And T.[LibraryId] In (
			Select	A.[LibraryId]
			From	[App_DataDictionary].[ModelLibrary] A
					Left Join [App_DataDictionary].[ModelLibrary] B
					On	A.[LibraryId] = B.[LibraryId] And
						A.[ModelId] <> B.[ModelId]
			Where	A.[ModelId] = @ModelId And B.[ModelId] is Null) Then Delete;

	With [Data] As (
		Select	@ModelId As [ModelId],
				[LibraryId]
		From	@Values)
	Merge [App_DataDictionary].[ModelLibrary] T
	Using [Data] D
	On	T.[ModelId] = D.[ModelId] And
		T.[LibraryId] = D.[LibraryId]
	When Not Matched by Target Then
		Insert ([ModelId], [LibraryId])
		Values ([ModelId], [LibraryId])
	When Not Matched by Source And T.[ModelId] = @ModelId Then Delete;

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