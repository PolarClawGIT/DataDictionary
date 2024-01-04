CREATE PROCEDURE [App_DataDictionary].[procSetModelSubjectArea]
		@ModelId UniqueIdentifier = Null,
		@SubjectAreaId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeModelSubjectArea] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ApplicationModelSubjectArea.
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
	Declare	@Values Table (
		[SubjectAreaId]          UniqueIdentifier NOT NULL,
		[ModelId]                UniqueIdentifier NOT NULL,
		[SubjectAreaParentId]    UniqueIdentifier NULL,
		[SubjectAreaTitle]       [App_DataDictionary].[typeTitle] NOT NULL,
		[SubjectAreaDescription] [App_DataDictionary].[typeDescription] NULL,
		Primary Key ([SubjectAreaId]))

	Declare @Delete Table (
		[SubjectAreaId] UniqueIdentifier Not Null,
		Primary Key ([SubjectAreaId]))

	Insert Into @Values
	Select	Coalesce(D.[SubjectAreaId], @SubjectAreaId, NewId()) As [SubjectAreaId],
			@ModelId As [ModelId],
			D.[SubjectAreaParentId],
			NullIf(Trim(D.[SubjectAreaTitle]), '') As [SubjectAreaTitle],
			NullIf(Trim(D.[SubjectAreaDescription]), '') As [SubjectAreaDescription]
	From	@Data D

	Insert Into @Delete
	Select	T.[SubjectAreaId]
	From	[App_DataDictionary].[ModelSubjectArea] T
			Left Join @Values S
			On	S.[SubjectAreaId] = T.[SubjectAreaId]
	Where	S.[SubjectAreaId] is Null And
			T.[SubjectAreaId] In (
			Select	A.[SubjectAreaId]
			From	[App_DataDictionary].[ModelSubjectArea] A
			Where	(@SubjectAreaId is Null Or @SubjectAreaId = A.[SubjectAreaId]) And
					(@ModelId is Null Or @ModelId = A.[ModelId]))

	-- Apply Changes
	Delete From [App_DataDictionary].[ModelAttribute]
	Where	[SubjectAreaId] In (
				Select	[SubjectAreaId]
				From	@Delete)
	Print FormatMessage ('Delete [App_DataDictionary].[ModelAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[ModelEntity]
	Where	[SubjectAreaId] In (
				Select	[SubjectAreaId]
				From	@Delete)
	Print FormatMessage ('Delete [App_DataDictionary].[ModelEntity]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[ModelProcess]
	Where	[SubjectAreaId] In (
				Select	[SubjectAreaId]
				From	@Delete)
	Print FormatMessage ('Delete [App_DataDictionary].[ModelProcess]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[ModelRelationship]
	Where	[SubjectAreaId] In (
				Select	[SubjectAreaId]
				From	@Delete)
	Print FormatMessage ('Delete [App_DataDictionary].[ModelRelationship]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Update [App_DataDictionary].[ModelSubjectArea]
	Set		[SubjectAreaParentId] = null
	Where	[SubjectAreaParentId] In (
				Select	[SubjectAreaId]
				From	@Delete)
	Print FormatMessage ('Update [App_DataDictionary].[ModelSubjectArea] (Parent): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[ModelSubjectArea]
	Where	[SubjectAreaId] In (
				Select	[SubjectAreaId]
				From	@Delete)
	Print FormatMessage ('Delete [App_DataDictionary].[ModelSubjectArea]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[SubjectAreaId],
				[SubjectAreaParentId],
				[SubjectAreaTitle],
				[SubjectAreaDescription]
		From	@Values
		Except
		Select	[SubjectAreaId],
				[SubjectAreaParentId],
				[SubjectAreaTitle],
				[SubjectAreaDescription]
		From	[App_DataDictionary].[ModelSubjectArea])
	Update [App_DataDictionary].[ModelSubjectArea]
	Set		[SubjectAreaParentId] = S.[SubjectAreaParentId],
			[SubjectAreaTitle] = S.[SubjectAreaTitle],
			[SubjectAreaDescription] = S.[SubjectAreaDescription]
	From	[Delta] S
			Inner Join [App_DataDictionary].[ModelSubjectArea] T
			On	S.[SubjectAreaId] = T.[SubjectAreaId]
	Print FormatMessage ('Update [App_DataDictionary].[ModelSubjectArea]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ModelSubjectArea] (
			[ModelId],
			[SubjectAreaId],
			[SubjectAreaParentId],
			[SubjectAreaTitle],
			[SubjectAreaDescription])
	Select	S.[ModelId],
			S.[SubjectAreaId],
			S.[SubjectAreaParentId],
			S.[SubjectAreaTitle],
			S.[SubjectAreaDescription]
	From	@Values S
			Left Join [App_DataDictionary].[ModelSubjectArea] T
			On	S.[SubjectAreaId] = T.[SubjectAreaId]
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