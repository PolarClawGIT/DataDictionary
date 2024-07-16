CREATE PROCEDURE [App_DataDictionary].[procSetDomainAttributeSubjectArea]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDomainAttributeSubjectArea] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ModelSubjectAttribute.
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

	-- Clean Data
	Declare @Values Table (
		[AttributeId]       UniqueIdentifier Not Null,
		[SubjectAreaId]		UniqueIdentifier Not Null,
		[NameSpaceId]		UniqueIdentifier Not Null,
		Primary Key ([AttributeId], [SubjectAreaId]))

	Declare @NameSpace [App_DataDictionary].[typeNameSpace]

	Insert Into @NameSpace
	Select	Null As [NameSpaceId],
			X.[NameSpace]
	From	@Data D
			Inner Join [App_DataDictionary].[ModelAttribute] A
			On	A.[ModelId] = @ModelId And
				D.[AttributeId] = A.[AttributeId] 
			Inner Join [App_DataDictionary].[ModelSubjectArea] S
			On	S.[ModelId] = @ModelId And
				D.[SubjectAreaId] = S.[SubjectAreaId]
			Outer Apply [App_DataDictionary].[funcGetNameSpace](S.[NameSpaceId]) J
			Outer Apply [App_DataDictionary].[funcSplitNameSpace](FormatMessage('%s.%s',J.[NameSpace],A.[MemberName])) X
	Group By X.[NameSpace]

	-- Need to create & assign the NameSpaceID's
	Exec [App_DataDictionary].[procSetModelNameSpace] @ModelId, @NameSpace

	;With [NameSpace] As (
		Select	M.[NameSpaceId],
				N.[NameSpace]
		From	[App_DataDictionary].[ModelNameSpace] M
				Cross Apply [App_DataDictionary].[funcGetNameSpace](M.[NameSpaceId]) N
		Where	(@ModelId is Null Or M.[ModelId] = @ModelId))
	Insert Into @Values
	Select	D.[AttributeId],
			D.[SubjectAreaId],
			N.[NameSpaceId]
	From	@Data D
			Inner Join [App_DataDictionary].[ModelAttribute] A
			On	A.[ModelId] = @ModelId And
				D.[AttributeId] = A.[AttributeId] 
			Inner Join [App_DataDictionary].[ModelSubjectArea] S
			On	S.[ModelId] = @ModelId And
				D.[SubjectAreaId] = S.[SubjectAreaId]
			Outer Apply [App_DataDictionary].[funcGetNameSpace](S.[NameSpaceId]) J
			Outer Apply [App_DataDictionary].[funcSplitNameSpace](FormatMessage('%s.%s',J.[NameSpace],A.[MemberName])) X
			Inner Join [NameSpace] N
			On	X.[NameSpace] = N.[NameSpace]
	Where	X.[IsBase] = 1


	-- Apply Changes
	Delete From [App_DataDictionary].[ModelSubjectAttribute]
	From	[App_DataDictionary].[ModelSubjectAttribute] T
			Left Join @Values V
			On	T.[AttributeId] = V.[AttributeId] And
				T.[SubjectAreaId] = V.[SubjectAreaId] And
				T.[ModelId] = @ModelId
	Where	V.[AttributeId] is Null And
			T.[AttributeId] In (
			Select	A.[AttributeId]
			From	[App_DataDictionary].[DomainAttribute] A
					Left Join [App_DataDictionary].[ModelAttribute] C
					On	A.[AttributeId] = C.[AttributeId]
			Where	(@AttributeId is Null Or @AttributeId = A.[AttributeId]) And
					(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[ModelSubjectAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	--[ModelId],
				[SubjectAreaId],
				[AttributeId],
				[NameSpaceId]
		From	@Values
		Except
		Select	[SubjectAreaId],
				[AttributeId],
				[NameSpaceId]
		From	[App_DataDictionary].[ModelSubjectAttribute]
		Where	[ModelId] = @ModelId)
	Update	[App_DataDictionary].[ModelSubjectAttribute]
	Set		[NameSpaceId] = S.[NameSpaceId]
	From	[App_DataDictionary].[ModelSubjectAttribute] T
			Inner Join [Delta] S
			On	T.[ModelId] = @ModelId And
				T.[AttributeId] = S.[AttributeId] And
				T.[SubjectAreaId] = S.[SubjectAreaId]
	Print FormatMessage ('Update [App_DataDictionary].[ModelSubjectAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ModelSubjectAttribute] ([ModelId], [AttributeId], [SubjectAreaId], [NameSpaceId])
	Select	@ModelId As [ModelId],
			V.[AttributeId],
			V.[SubjectAreaId],
			V.[NameSpaceId]
	From	@Values V
			Left Join [App_DataDictionary].[ModelSubjectAttribute] T
			On	V.[AttributeId] = T.[AttributeId] And
				V.[SubjectAreaId] = T.[SubjectAreaId] And
				T.[ModelId] = @ModelId
	Where	T.[AttributeId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ModelSubjectAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
