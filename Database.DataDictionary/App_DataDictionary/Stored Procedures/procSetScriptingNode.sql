CREATE PROCEDURE [App_DataDictionary].[procSetScriptingNode]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeScriptingNode] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ScriptingNode.
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
		[NodeId]				UniqueIdentifier NOT NULL,
		[TemplateId]            UniqueIdentifier NOT NULL,
		[PropertyScope]         [App_DataDictionary].[typeScopeName] NOT NULL,
		[PropertyName]          [App_DataDictionary].[typeNameSpaceMember] NOT NULL,
		[NodeName]				[App_DataDictionary].[typeNameSpaceMember] NOT Null,
		[NodeValueAs]			NVarChar(50) Not Null,
		Primary Key ([NodeId]))

	Insert Into @Values
	Select	X.[NodeId],
			[TemplateId],
			NullIf(Trim([PropertyScope]),'') As [PropertyScope],
			NullIf(Trim([PropertyName]),'') As [PropertyName],
			NullIf(Trim([NodeName]),'') As [NodeName],
			NullIf(Trim([NodeValueAs]),'') As [NodeValueAs]
	From	@Data D
			Cross apply (Select	Coalesce(D.[NodeId], NewId()) As [NodeId]) X

	-- Apply Changes
	Delete From [App_DataDictionary].[ScriptingAttribute]
	From	[App_DataDictionary].[ScriptingNode] P
			Inner Join [App_DataDictionary].[ScriptingAttribute] T
			On	P.[NodeId] = T.[NodeId]
			Left Join @Values S
			On	T.[NodeId] = S.[NodeId]
	Where	S.[NodeId] is Null And
			P.[TemplateId] In (
				Select	[TemplateId]
				From	[App_DataDictionary].[ModelScripting]
				Where	[ModelId] = @ModelId
				Union
				Select	@TemplateId As [TemplateId]
				Where	@TemplateId is Not Null)
	Print FormatMessage ('Delete [App_DataDictionary].[ScriptingNodeAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[ScriptingNode]
	From	[App_DataDictionary].[ScriptingNode] T
			Left Join @Values S
			On	T.[NodeId] = S.[NodeId]
	Where	S.[NodeId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	[App_DataDictionary].[ModelScripting]
				Where	[ModelId] = @ModelId
				Union
				Select	@TemplateId As [TemplateId]
				Where	@TemplateId is Not Null)
	Print FormatMessage ('Delete [App_DataDictionary].[ScriptingNode]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[NodeId],
				[TemplateId],
				[PropertyScope],
				[PropertyName],
				[NodeName],
				[NodeValueAs]
		From	@Values
		Except
		Select	[NodeId],
				[TemplateId],
				[PropertyScope],
				[PropertyName],
				[NodeName],
				[NodeValueAs]
		From	[App_DataDictionary].[ScriptingNode])
	Update [App_DataDictionary].[ScriptingNode]
	Set		[PropertyScope] = S.[PropertyScope],
			[PropertyName] = S.[PropertyName],
			[NodeName] = S.[NodeName],
			[NodeValueAs] = S.[NodeValueAs]
	From	[App_DataDictionary].[ScriptingNode] T
			Inner Join [Delta] S
			On	T.[NodeId] = S.[NodeId]
	Print FormatMessage ('Update [App_DataDictionary].[ScriptingNode]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ScriptingNode] (
			[NodeId],
			[TemplateId],
			[PropertyScope],
			[PropertyName],
			[NodeName],
			[NodeValueAs])
	Select	S.[NodeId],
			S.[TemplateId],
			S.[PropertyScope],
			S.[PropertyName],
			S.[NodeName],
			S.[NodeValueAs]
	From	@Values S
			Left Join [App_DataDictionary].[ScriptingNode] T
			On	S.[NodeId] = T.[NodeId]
	Where	T.[TemplateId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ScriptingNode]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
/*
Begin Try;
	Begin Transaction;
	Set NoCount On;

	Declare @ModelID UniqueIdentifier = (Select [ModelId] from [App_DataDictionary].[Model] Where [ModelTitle] = 'Unit Test')
	Declare @Data [App_DataDictionary].[typeScriptingTemplate]
	Declare @Element [App_DataDictionary].[typeScriptingNode]

	Insert Into @Data ([TemplateId], [TemplateTitle])
	Values (NewId(),'Test Template')

	Insert into @Element ([NodeId], [TemplateId], [PropertyScope], [PropertyName],[DataAs])
	Select	NewId(),
			D.[TemplateId],
			'Dummy','Dummy',
			'Text'
	From	@Data D

	Exec [App_DataDictionary].[procSetScriptingTemplate] @ModelID = @ModelID, @Data = @Data
	Exec [App_DataDictionary].[procSetScriptingNode] @ModelID = @ModelID, @Data = @Element

	Update @Element
	Set	[ElementName] = 'Fun'

	Exec [App_DataDictionary].[procSetScriptingNode] @ModelID = @ModelID, @Data = @Element

	Delete From @Data

	Exec [App_DataDictionary].[procSetScriptingTemplate] @ModelID = @ModelID, @Data = @Data

	Select	*
	From	[App_DataDictionary].[ScriptingTemplate]


	-- By default, throw and error and exit without committing
;	Throw 50000, 'Abort process, comment out this line when ready to actual Commit the transaction',255;
	
	Commit Transaction;
	Print 'Commit Issued';
End Try
Begin Catch
	Print FormatMessage ('*** Error Report: %s ***', Object_Name(@@ProcID));
	Print FormatMessage (' Message- %s', ERROR_MESSAGE());
	Print FormatMessage (' Number- %i', ERROR_NUMBER());
	Print FormatMessage (' Severity- %i', ERROR_SEVERITY());
	Print FormatMessage (' State- %i', ERROR_STATE());
	Print FormatMessage (' Procedure- %s', ERROR_PROCEDURE());
	Print FormatMessage (' Line- %i', ERROR_LINE());
	Print FormatMessage (' @@TranCount - %i', @@TranCount);
	Print FormatMessage (' @@NestLevel - %i', @@NestLevel);
	Print FormatMessage (' Original_Login - %s', Original_Login());
	Print FormatMessage (' Current_User - %s', Current_User);
	Print FormatMessage (' XAct_State - %i', XAct_State());
	Print '--- Debug Data ---';

	-- Rollback Transaction
	Print 'Rollback Issued';
	Rollback Transaction;
	--Throw;
End Catch;
*/