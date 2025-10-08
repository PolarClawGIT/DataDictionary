CREATE PROCEDURE [AppScript].[procSetTemplateParentNode]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [AppScript].[udttTemplateParentNode] ReadOnly
AS
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on TemplateElement.
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

	If Exists (
		Select	1
		From	@Data D
				Cross Apply [AppSecurity].[funcScriptingAuthorization](IsNull([TemplateId],@TemplateId), 0))
	Throw 601020, 'Template Not Authorized', 2;

	-- Clean the Data, helps performance
	Declare @Values Table (
			[NodeId]		UniqueIdentifier NOT NULL,
			[ParentNodeId]	UniqueIdentifier Not NULL,
			[TemplateId]	UniqueIdentifier NOT NULL,
			[NodePath]		[AppGeneral].[uddtNameSpacePath] Null,
			[ParentPath]	[AppGeneral].[uddtNameSpacePath] Null)
	Declare @NullPath		[AppGeneral].[uddtNameSpacePath] = null

	-- Root Nodes
	;With [Nodes] As (
		Select	N.[NodeId],
				Convert(UniqueIdentifier, Null) As [ParentNodeId],
				N.[TemplateId],
				FormatMessage('[%s]', N.[NodeName]) As [NodePath],
				@NullPath As [ParentPath]
		From	[AppScript].[TemplateNode] N
				Left Join @Data D
				On	N.[NodeId] = D.[NodeId]
		Where	D.[NodeId] is Null And
				(@TemplateId is Not Null Or @ModelId is Not Null) And
				(@TemplateId is Null Or @TemplateId = N.[TemplateId])  And
				(@ModelId is Null Or N.[TemplateId] In (
					Select	[TemplateId]
					From	[AppScript].[ScriptingModel]
					Where	[ModelId] = @ModelId))
		Union -- Child Nodes
		Select	D.[NodeId],
				D.[ParentNodeId] As [ParentNodeId],
				N.[TemplateId],
				FormatMessage('%s.[%s]', P.[QualifiedName], N.[NodeName]) As [NodePath],
				P.[QualifiedName] As [ParentPath]
		From	@Data D
				Inner Join [AppScript].[TemplateNode] N
				On	D.[NodeId] = N.[NodeId]
				Cross Apply [AppGeneral].[funcParseName](D.[ParentPath]) P
		Where	P.[IsBase] = 1)
	Insert Into @Values
	Select	N.[NodeId],
			IsNull(N.[ParentNodeId], P.[NodeId]) As [ParentNodeId],
			N.[TemplateId],
			N.[NodePath],
			N.[ParentPath]
	From	[Nodes] N
			Inner Join [Nodes] P
			On	N.[ParentPath] = P.[NodePath]

	-- Apply Changes
	Delete From [AppScript].[TemplateParentNode]
	From	[AppScript].[TemplateParentNode] T
			Left Join @Values S
			On	T.[NodeId] = S.[NodeId] And
				T.[ParentNodeId] = S.[ParentNodeId] And
				T.[TemplateId] = S.[TemplateId]
	Where	S.[NodeId] is Null And
			(@TemplateId is Not Null Or @ModelId is Not Null) And
			(@TemplateId is Null Or @TemplateId = T.[TemplateId])  And
			(@ModelId is Null Or T.[TemplateId] In (
				Select	[TemplateId]
				From	[AppScript].[ScriptingModel]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppScript].[TemplateParentNode]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[TemplateParentNode] (
			[NodeId],
			[ParentNodeId],
			[TemplateId])
	Select	S.[NodeId],
			S.[ParentNodeId],
			S.[TemplateId]
	From	@Values S
			Left Join [AppScript].[TemplateParentNode] T
			On	T.[NodeId] = S.[NodeId] And
				T.[ParentNodeId] = S.[ParentNodeId] And
				T.[TemplateId] = S.[TemplateId]
	Where	T.[NodeId] is Null
	Print FormatMessage ('Insert [AppScript].[TemplateParentNode]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
