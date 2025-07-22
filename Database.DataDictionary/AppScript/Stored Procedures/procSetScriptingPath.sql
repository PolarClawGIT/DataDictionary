/*Obsolete

CREATE PROCEDURE [AppScript].[procSetScriptingPath]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [AppScript].[udttScriptingPath] ReadOnly
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
		[TemplateId]		UniqueIdentifier NOT NULL,
		[NameSpaceId]		UniqueIdentifier NOT NULL,
		[ScopeName]			[AppGeneral].[uddtScopeName] NOT NULL,
		Primary Key ([TemplateId], [NameSpaceId]))

	Declare @NameSpace Table (
		[NameSpaceId]		UniqueIdentifier NOT NULL,
		[NameSpace]			[AppGeneral].[uddtNameSpacePath] NOT NULL,
		[MemberName]		[AppGeneral].[uddtNameSpaceMember] NOT NULL,
		[ParentName]		[AppGeneral].[uddtNameSpacePath] NULL,
		Primary Key ([NameSpaceId]))

	-- Resolve NameSpaceId
	;With [Summary] As (
		Select	C.[QualifiedName],
				C.[ParentName],
				C.[MemberName]
		From	@Data D
				Cross Apply [AppGeneral].[funcParseName](D.[NameSpace]) C
		Group By C.[QualifiedName],
				C.[ParentName],
				C.[MemberName]),
	[NameSpaces] As (
		Select	Coalesce([AppScript].[funcNameSpaceId]([QualifiedName]), NewId()) As [NameSpaceId],
				[QualifiedName],
				[ParentName],
				[MemberName]
		From	[Summary])
	Insert Into @NameSpace
	Select	[NameSpaceId],
			[QualifiedName] As [NameSpace],
			[MemberName],
			[ParentName]
	From	[NameSpaces] N
	Print FormatMessage ('Insert @NameSpace: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Apply Changes
	Insert Into [AppScript].[ScriptingNameSpace] (
			[NameSpaceId],
			[MemberName],
			[ParentNameSpaceId])
	Select	N.[NameSpaceId],
			N.[MemberName],
			P.[NameSpaceId] As [ParentNameSpaceId]
	From	@NameSpace N
			Left Join @NameSpace P
			On	N.[ParentName] = P.[NameSpace]
			Left Join [AppScript].[ScriptingNameSpace] T
			On	N.[NameSpaceId] = T.[NameSpaceId]
	Where	T.[NameSpaceId] is Null
	Print FormatMessage ('Insert [AppScript].[ScriptingNameSpace]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into @Values -- Need [AppScript].[ScriptingNameSpace] updated first
	Select	D.[TemplateId],
			[AppScript].[funcNameSpaceId](D.[NameSpace]) As [NameSpaceId],
			D.[NameSpaceScope]
	From	@Data D
	Where	(@TemplateId is Null Or @TemplateId = D.[TemplateId]) And
			(@ModelId is Null Or D.[TemplateId] In (
				Select	[TemplateId]
				From	[AppScript].[ScriptingModel]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Insert @Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppScript].[ScriptingPath]
	From	[AppScript].[ScriptingPath] T
			Left Join @Values S
			On	T.[NameSpaceId] = S.[NameSpaceId] And
				T.[TemplateId] = S.[TemplateId]
	Where	S.[NameSpaceId] is Null And
			T.[TemplateId] In (
				Select	[TemplateId]
				From	[AppScript].[ScriptingModel]
				Where	[ModelId] = @ModelId
				Union
				Select	@TemplateId As [TemplateId]
				Where	@TemplateId is Not Null)
	Print FormatMessage ('Delete [AppScript].[ScriptingPath]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[ScriptingPath] (
			[TemplateId],
			[NameSpaceId],
			[NameSpaceScope])
	Select	S.[TemplateId],
			S.[NameSpaceId],
			S.[ScopeName]
	From	@Values S
			Left Join [AppScript].[ScriptingPath] T
			On	S.[TemplateId] = T.[TemplateId] And
				S.[NameSpaceId] = T.[NameSpaceId]
	Where	T.[TemplateId] is Null
	Print FormatMessage ('Insert [AppScript].[ScriptingPath]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
*/