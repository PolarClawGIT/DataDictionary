CREATE PROCEDURE [App_DataDictionary].[procSetDomainSubjectArea]
		@ModelId UniqueIdentifier = Null,
		@SubjectAreaId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDomainSubjectArea] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DomainSubjectArea.
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

	If @ModelId is Null and @SubjectAreaId is Null
	Throw 50000, '@ModelId or @SubjectAreaId must be specified', 1;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[SubjectAreaId] UniqueIdentifier Not Null,
		[SubjectAreaTitle] [App_DataDictionary].[typeTitle] Not Null,
		[SubjectAreaDescription] [App_DataDictionary].[typeDescription] Null,
		Primary Key ([SubjectAreaId]))

	Insert Into @Values
	Select	Coalesce(A.[SubjectAreaId], D.[SubjectAreaId], @SubjectAreaId, NewId()) As [SubjectAreaId],
			NullIf(Trim(D.[SubjectAreaTitle]),'') As [SubjectAreaTitle],
			NullIf(Trim(D.[SubjectAreaDescription]),'') As [SubjectAreaDescription]
	From	@Data D
			Left Join [App_DataDictionary].[ModelSubjectArea_AK] A
			On	@ModelId = A.[ModelId] And
				NullIf(Trim(D.[SubjectAreaTitle]),'') = A.[SubjectAreaTitle]

	-- Apply Changes
	Update [App_DataDictionary].[ModelAttribute]
	Set		[SubjectAreaId] = Null
	From	[App_DataDictionary].[ModelAttribute] T
			Left Join @Values S
			On	T.[SubjectAreaId] = S.[SubjectAreaId]
	Where	S.[SubjectAreaId] is Null And
			(@SubjectAreaId is Null or T.[SubjectAreaId] = @SubjectAreaId) And
			(@ModelId is Null or T.[ModelId] = @ModelId)
	Print FormatMessage ('Update [App_DataDictionary].[ModelAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Update [App_DataDictionary].[ModelEntity]
	Set		[SubjectAreaId] = Null
	From	[App_DataDictionary].[ModelEntity] T
			Left Join @Values S
			On	T.[SubjectAreaId] = S.[SubjectAreaId]
	Where	S.[SubjectAreaId] is Null And
			(@SubjectAreaId is Null or T.[SubjectAreaId] = @SubjectAreaId) And
			(@ModelId is Null or T.[ModelId] = @ModelId)
	Print FormatMessage ('Update [App_DataDictionary].[ModelEntity]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[ModelSubjectArea]
	From	[App_DataDictionary].[ModelSubjectArea] T
			Left Join @Values S
			On	T.[SubjectAreaId] = S.[SubjectAreaId]
	Where	S.[SubjectAreaId] is Null And
			(@SubjectAreaId is Null or T.[SubjectAreaId] = @SubjectAreaId) And
			(@ModelId is Null or T.[ModelId] = @ModelId)
	Print FormatMessage ('Delete [App_DataDictionary].[ModelSubjectArea]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DomainSubjectArea]
	From	[App_DataDictionary].[DomainSubjectArea] T
			Left Join @Values S
			On	T.[SubjectAreaId] = S.[SubjectAreaId]
	Where	S.[SubjectAreaId] is Null And
			(@SubjectAreaId is Null or T.[SubjectAreaId] = @SubjectAreaId)
	Print FormatMessage ('Delete [App_DataDictionary].[DomainSubjectArea]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[SubjectAreaId],
				[SubjectAreaTitle],
				[SubjectAreaDescription]
		From	@Values
		Except 
		Select	[SubjectAreaId],
				[SubjectAreaTitle],
				[SubjectAreaDescription]
		From	[App_DataDictionary].[DomainSubjectArea])
	Update [App_DataDictionary].[DomainSubjectArea]
	Set		[SubjectAreaTitle] = S.[SubjectAreaTitle],
			[SubjectAreaDescription] = S.[SubjectAreaDescription]
	From	[App_DataDictionary].[DomainSubjectArea] T
			Inner Join [Delta] S
			On	T.[SubjectAreaId] = S.[SubjectAreaId]
	Print FormatMessage ('Update [App_DataDictionary].[DomainSubjectArea]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DomainSubjectArea] (
				[SubjectAreaId],
				[SubjectAreaTitle],
				[SubjectAreaDescription])
	Select	S.[SubjectAreaId],
			S.[SubjectAreaTitle],
			S.[SubjectAreaDescription]
	From	@Values S
			Left Join [App_DataDictionary].[DomainSubjectArea] T
			On	S.[SubjectAreaId] = T.[SubjectAreaId]
	Where	T.[SubjectAreaId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DomainSubjectArea]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ModelSubjectArea] (
			[ModelId],
			[SubjectAreaId])
	Select	@ModelId As [ModelId],
			S.[SubjectAreaId]
	From	@Values S
			Left Join [App_DataDictionary].[ModelSubjectArea] T
			On	@ModelId = T.[ModelId] And
				S.[SubjectAreaId] = T.[SubjectAreaId]
	Where	T.[SubjectAreaId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ModelSubjectArea]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
	Print FormatMessage (' @ModelId- %s',Convert(NVarChar(50),@ModelId))

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