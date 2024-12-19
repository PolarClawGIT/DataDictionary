CREATE PROCEDURE [AppModel].[procSetSubjectArea]
		@ModelId UniqueIdentifier = Null,
		@SubjectAreaId UniqueIdentifier = Null,
		@Data [AppModel].[typeSubjectArea] ReadOnly
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
	Declare @Values Table (
		[SubjectAreaId]          UniqueIdentifier NOT NULL,
		[SubjectAreaTitle]       [App_DataDictionary].[typeTitle] Not NULL,
		[SubjectAreaDescription] [App_DataDictionary].[typeDescription] NULL,
		[ModelId]				 UniqueIdentifier Not NULL,
		[SubjectName]            [AppModel].[typeQualifiedName] Not Null
		Primary Key ([SubjectAreaId]),
		Unique ([SubjectAreaTitle]))

	Insert Into @Values
	Select	Coalesce(D.[SubjectAreaId], H.[SubjectAreaId], NewId()) As [SubjectAreaId],
			NullIf(Trim(D.[SubjectAreaTitle]),'') As [SubjectAreaTitle],
			NullIf(Trim(D.[SubjectAreaDescription]),'') As [SubjectAreaDescription],
			IsNull(H.[ModelId], @ModelId) As [ModelId],
			N.[SubjectName]
	From	@Data D
			Left Join [AppModel].[SubjectAreaHs] H
			On	(D.[SubjectAreaId] = H.[SubjectAreaId] Or
				 (H.[ModelId] = @ModelId And
				  D.[SubjectAreaTitle] = H.[SubjectAreaTitle]))
			Cross Apply (
				Select	[QualifiedName] As [SubjectName]
				From	[AppModel].[funcParseName](D.[SubjectName])) N
	Where	(@ModelId is Null Or @ModelId = H.[ModelId]) And
			(@SubjectAreaId is Null Or @SubjectAreaId = Coalesce(D.[SubjectAreaId], H.[SubjectAreaId]))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppModel].[AttributeSubjectArea]
	From	[AppModel].[AttributeSubjectArea] T
			Left Join @Values S
			On	T.[SubjectAreaId] = S.[SubjectAreaId]
	Where	S.[SubjectAreaId] is Null And
			(@SubjectAreaId is Not Null Or @ModelId is Not Null) And
			(@SubjectAreaId is Null Or @SubjectAreaId = T.[SubjectAreaId]) And
			(@ModelId is Null Or @ModelId = T.[ModelId])
	Print FormatMessage ('Delete [AppModel].[AttributeSubjectArea] (SubjectArea): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[EntitySubjectArea]
	From	[AppModel].[EntitySubjectArea] T
			Left Join @Values S
			On	T.[SubjectAreaId] = S.[SubjectAreaId]
	Where	S.[SubjectAreaId] is Null And
			(@SubjectAreaId is Not Null Or @ModelId is Not Null) And
			(@SubjectAreaId is Null Or @SubjectAreaId = T.[SubjectAreaId]) And
			(@ModelId is Null Or @ModelId = T.[ModelId])
	Print FormatMessage ('Delete [AppModel].[EntitySubjectArea] (SubjectArea): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[ProcessSubjectArea]
	From	[AppModel].[ProcessSubjectArea] T
			Left Join @Values S
			On	T.[SubjectAreaId] = S.[SubjectAreaId]
	Where	S.[SubjectAreaId] is Null And
			(@SubjectAreaId is Not Null Or @ModelId is Not Null) And
			(@SubjectAreaId is Null Or @SubjectAreaId = T.[SubjectAreaId]) And
			(@ModelId is Null Or @ModelId = T.[ModelId])
	Print FormatMessage ('Delete [AppModel].[ProcessSubjectArea] (SubjectArea): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[RelationshipSubjectArea]
	From	[AppModel].[RelationshipSubjectArea] T
			Left Join @Values S
			On	T.[SubjectAreaId] = S.[SubjectAreaId]
	Where	S.[SubjectAreaId] is Null And
			(@SubjectAreaId is Not Null Or @ModelId is Not Null) And
			(@SubjectAreaId is Null Or @SubjectAreaId = T.[SubjectAreaId]) And
			(@ModelId is Null Or @ModelId = T.[ModelId])
	Print FormatMessage ('Delete [AppModel].[RelationshipSubjectArea] (SubjectArea): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[SubjectAreaId],
				[SubjectAreaTitle],
				[SubjectAreaDescription],
				[ModelId],
				[SubjectName]
		From	@Values S
		Except
		Select	[SubjectAreaId],
				[SubjectAreaTitle],
				[SubjectAreaDescription],
				[ModelId],
				[SubjectName]
		From	[AppModel].[SubjectArea])
	Update [AppModel].[SubjectArea]
	Set		[SubjectAreaTitle] = S.[SubjectAreaTitle],
			[SubjectAreaDescription] = S.[SubjectAreaDescription],
			[SubjectName] = S.[SubjectName]
	From	[Delta] S
			Inner Join [AppModel].[SubjectArea] T
			On	S.[SubjectAreaId] = T.[SubjectAreaId]
	Print FormatMessage ('Update [AppModel].[SubjectArea]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[SubjectArea] (
			[SubjectAreaId],
			[SubjectAreaTitle],
			[SubjectAreaDescription],
			[ModelId],
			[SubjectName])
	Select	S.[SubjectAreaId],
			S.[SubjectAreaTitle],
			S.[SubjectAreaDescription],
			S.[ModelId],
			S.[SubjectName]
	From	@Values S
			Left Join [AppModel].[SubjectArea] T
			On	S.[SubjectAreaId] = T.[SubjectAreaId]
	Where	T.[SubjectAreaId] is Null
	Print FormatMessage ('Insert [AppModel].[SubjectArea]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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

