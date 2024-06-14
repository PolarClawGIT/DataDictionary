CREATE PROCEDURE [App_DataDictionary].[procSetScriptingAttribute]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeScriptingAttribute] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ScriptingAttribute.
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
		[AttributeId]			UniqueIdentifier NOT NULL,
		[NodeId]	            UniqueIdentifier NOT NULL,
		[AttributeName]			NVarChar(50) NOT NULL,
		[AttributeValue]		NVarChar(250) NULL,
		[PropertyId]			UniqueIdentifier NULL,
		Primary Key ([AttributeId]))

	Insert Into @Values
	Select	X.[AttributeId],
			[NodeId],
			NullIf(Trim([AttributeName]),'') As [AttributeName],
			NullIf(Trim([AttributeValue]),'') As [AttributeValue],
			[PropertyId]
	From	@Data D
			Cross apply (Select	Coalesce(D.[AttributeId], NewId()) As [AttributeId]) X

	-- Apply Changes
	Delete From [App_DataDictionary].[ScriptingAttribute]
	From	[App_DataDictionary].[ScriptingNode] P
			Inner Join [App_DataDictionary].[ScriptingAttribute] T
			On	P.[NodeId] = T.[NodeId]
			Left Join @Values S
			On	T.[AttributeId] = S.[AttributeId]
	Where	S.[NodeId] is Null And
			P.[TemplateId] In (
				Select	[TemplateId]
				From	[App_DataDictionary].[ModelScripting]
				Where	[ModelId] = @ModelId
				Union
				Select	@TemplateId As [TemplateId]
				Where	@TemplateId is Not Null)
	Print FormatMessage ('Delete [App_DataDictionary].[ScriptingAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[AttributeId],
				[NodeId],
				[AttributeName],
				[AttributeValue],
				[PropertyId]
		From	@Values
		Except
		Select	[AttributeId],
				[NodeId],
				[AttributeName],
				[AttributeValue],
				[PropertyId]
		From	[App_DataDictionary].[ScriptingAttribute])
	Update [App_DataDictionary].[ScriptingAttribute]
	Set		[AttributeName] = S.[AttributeName],
			[AttributeValue] = S.[AttributeValue],
			[PropertyId] = S.[PropertyId]
	From	[App_DataDictionary].[ScriptingAttribute] T
			Inner Join [Delta] S
			On	T.[AttributeId] = S.[AttributeId]
	Print FormatMessage ('Update [App_DataDictionary].[ScriptingAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ScriptingAttribute] (
			[AttributeId],
			[NodeId],
			[AttributeName],
			[AttributeValue],
			[PropertyId])
	Select	S.[AttributeId],
			S.[NodeId],
			S.[AttributeName],
			S.[AttributeValue],
			S.[PropertyId]
	From	@Values S
			Left Join [App_DataDictionary].[ScriptingAttribute] T
			On	S.[AttributeId] = T.[AttributeId]
	Where	T.[AttributeId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ScriptingAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
