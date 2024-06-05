CREATE PROCEDURE [App_DataDictionary].[procSetScriptingPath]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeScriptingPath] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ScriptingPath.
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

	Declare @Values Table (
		[TemplateId]        UniqueIdentifier NOT NULL,
		[NameSpaceId]       UniqueIdentifier NOT NULL,
		[PathScope]         [App_DataDictionary].[typeScopeName] NOT NULL,
		Primary Key ([TemplateId], [NameSpaceId]))
		
	Declare @NameSpace [App_DataDictionary].[typeNameSpace]

	Insert Into @NameSpace
	Select	Null As [NameSpaceId],
			[PathName] As [NameSpace]
	From	@Data
	Group By [PathName]

	-- Need to create & assign the NameSpaceID's
	Exec [App_DataDictionary].[procSetModelNameSpace] @ModelId, @NameSpace

	;With [NameSpace] As (
		Select	M.[NameSpaceId],
				N.[NameSpace]
		From	[App_DataDictionary].[ModelNameSpace] M
				Cross Apply [App_DataDictionary].[funcGetNameSpace](M.[NameSpaceId]) N
		Where	(@ModelId is Null Or M.[ModelId] = @ModelId))
	Insert Into @Values
	Select	Coalesce(D.[TemplateId], @TemplateId, NewId()) As [TemplateId],
			N.[NameSpaceId],
			D.[PathScope]
	From	@Data D
			Cross Apply [App_DataDictionary].[funcSplitNameSpace](D.[PathScope]) C
			Inner Join [NameSpace] N
			On	C.[NameSpace] = N.[NameSpace] And
				C.[IsBase] = 1

	-- Apply Changes
	Delete From [App_DataDictionary].[ScriptingPath]
	From	[App_DataDictionary].[ScriptingPath] T
			Left Join @Values S
			On	T.[NameSpaceId] = S.[NameSpaceId] And
				T.[TemplateId] = S.[TemplateId]
	Where	S.[NameSpaceId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	[App_DataDictionary].[ModelScripting]
				Where	[ModelId] = @ModelId
				Union
				Select	@TemplateId As [TemplateId]
				Where	@TemplateId is Not Null)
	Print FormatMessage ('Delete [App_DataDictionary].[ScriptingPath]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ScriptingPath] (
			[TemplateId],
			[NameSpaceId],
			[PathScope])
	Select	S.[TemplateId],
			S.[NameSpaceId],
			S.[PathScope]
	From	@Values S
			Left Join [App_DataDictionary].[ScriptingPath] T
			On	S.[TemplateId] = T.[TemplateId] And
				S.[NameSpaceId] = T.[NameSpaceId]
	Where	T.[TemplateId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ScriptingPath]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
