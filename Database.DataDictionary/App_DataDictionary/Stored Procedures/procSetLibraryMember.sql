CREATE PROCEDURE [App_DataDictionary].[procSetLibraryMember]
		@ModelId UniqueIdentifier = null,
		@LibraryId UniqueIdentifier = null,
		@Data [App_DataDictionary].[typeLibraryMember] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on LibraryMember.
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
	Declare @Values [App_DataDictionary].[typeLibraryMember]
	Insert Into @Values
	Select	P.[LibraryId],
			D.[MemberId],
			D.[ParentMemberId],
			P.[AssemblyName],
			null As [NameSpace], -- Not used. ParentMemberId determines NameSpace.
			NullIf(Trim(D.[MemberName]),'') As [MemberName],
			NullIf(Trim(D.[MemberType]),'') As [MemberType],
			D.[MemberData]
	From	@Data D
			Left Join [App_DataDictionary].[LibrarySource] P
			On	D.[AssemblyName] = P.[AssemblyName]
	Where	(@LibraryId is Null or P.[LibraryId] = @LibraryId) And
			(@ModelId is Null or @ModelId In (
				Select	[ModelId]
				From	[App_DataDictionary].[ModelLibrary]
				Where	(@LibraryId is Null Or [LibraryId] = @LibraryId)))

	-- Validation
	If @ModelId is Null and @LibraryId is Null
	Throw 50000, '@ModelId or @LibraryId must be specified', 1;

	;With [Delta] As (
		Select	[LibraryId],
				[MemberId],
				[ParentMemberId],
				[MemberName],
				[MemberType],
				Convert(NVarChar(Max),[MemberData]) As [MemberData]
		From	@Values
		Except
		Select	[LibraryId],
				[MemberId],
				[ParentMemberId],
				[MemberName],
				[MemberType],
				Convert(NVarChar(Max),[MemberData]) As [MemberData]
		From	[App_DataDictionary].[LibraryMember]),
	[Data] As (
		Select	V.[LibraryId],
				V.[MemberId],
				V.[ParentMemberId],
				V.[MemberName],
				V.[MemberType],
				V.[MemberData],
				IIF(D.[MemberId] is Null,0, 1) As [IsDiffrent]
		From	@Values V
				Left Join [Delta] D
				On	V.[LibraryId] = D.[LibraryId] And
					V.[MemberId] = D.[MemberId])
	Merge [App_DataDictionary].[LibraryMember] As T
	Using [Data] As S
	On	T.[LibraryId] = S.[LibraryId] And
		T.[MemberId] = S.[MemberId]
	When Matched and [IsDiffrent] = 1 Then Update Set
		[ParentMemberId] = S.[ParentMemberId],
		[MemberName] = S.[MemberName],
		[MemberType] = S.[MemberType],
		[MemberData] = S.[MemberData]
	When Not Matched by Target Then
		Insert ([LibraryId], [MemberId], [ParentMemberId], [MemberName], [MemberType], [MemberData])
		Values ([LibraryId], [MemberId], [ParentMemberId], [MemberName], [MemberType], [MemberData])
	When Not Matched by Source And
		(@LibraryId = T.[LibraryId] Or
		 T.[LibraryId] In (
			Select	[LibraryId]
			From	[App_DataDictionary].[ModelLibrary]
			Where	[ModelId] = @ModelId))
		Then Delete;
	Print FormatMessage ('Merge [App_DataDictionary].[LibraryMember]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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