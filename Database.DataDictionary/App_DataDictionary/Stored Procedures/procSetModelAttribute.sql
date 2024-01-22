CREATE PROCEDURE [App_DataDictionary].[procSetModelAttribute]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeModelAttribute] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ApplicationModelAttribute.
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
		[ModelId]               UniqueIdentifier NOT NULL,
		[AttributeId]           UniqueIdentifier NOT NULL,
		[SubjectAreaId]         UniqueIdentifier NULL,
		Primary Key ([AttributeId]))

	Insert Into @Values
	Select	M.[ModelId],
			A.[AttributeId],
			S.[SubjectAreaId]
	From	@Data D
			Inner Join [App_DataDictionary].[DomainAttribute] A
			On	IsNull(D.[AttributeId], @AttributeId)  = A.[AttributeId]
			Inner Join [App_DataDictionary].[Model] M
			On	@ModelId = M.[ModelId]
			Left Join [App_DataDictionary].[ModelSubjectArea] S
			On	D.[SubjectAreaId] = S.[SubjectAreaId]

	-- Apply Changes
	Delete From [App_DataDictionary].[ModelAttribute]
	From	[App_DataDictionary].[ModelAttribute] T
			Left Join @Values S
			On	T.[ModelId] = S.[ModelId] And
				T.[AttributeId] = S.[AttributeId]
	Where	T.[ModelId] = @ModelId And
			S.[AttributeId] is Null
	Print FormatMessage ('Delete [App_DataDictionary].[ModelAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[ModelId],
				[AttributeId],
				[SubjectAreaId]
		From	@Values
		Except
		Select	[ModelId],
				[AttributeId],
				[SubjectAreaId]
		From	[App_DataDictionary].[ModelAttribute])
	Update [App_DataDictionary].[ModelAttribute]
	Set		[SubjectAreaId] = S.[SubjectAreaId]
	From	[Delta] S
			Inner Join [App_DataDictionary].[ModelAttribute] T
			On	S.[ModelId] = T.[ModelId] And
				S.[AttributeId] = T.[AttributeId]
	Print FormatMessage ('Update [App_DataDictionary].[ModelAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ModelAttribute] (
			[ModelId],
			[AttributeId],
			[SubjectAreaId])
	Select	S.[ModelId],
			S.[AttributeId],
			S.[SubjectAreaId]
	From	@Values S
			Left Join [App_DataDictionary].[ModelAttribute] T
			On	S.[ModelId] = T.[ModelId] And
				S.[AttributeId] = T.[AttributeId]
	Where	T.[AttributeId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ModelAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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