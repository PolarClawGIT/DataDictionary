CREATE PROCEDURE [Obsolete].[procSetTemplateNodeOwner]
		@ModelId UniqueIdentifier = Null,
		@TemplateId UniqueIdentifier = Null,
		@Data [Obsolete].[udttTemplateNodeOwner] ReadOnly
AS
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on TemplateNodeOwner.
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
			[NodeOwnerId]	UniqueIdentifier Not NULL,
			[TemplateId]	UniqueIdentifier NOT NULL)
	Declare @NullPath		[AppGeneral].[uddtPath] = null

	Insert Into @Values
	Select	[NodeId],
			[NodeOwnerId],
			IsNull([TemplateId], @TemplateId) As [TemplateId]
	From	@Data D
	Where	(@TemplateId is Null And [TemplateId] is Not Null) Or
			(@TemplateId is Not Null And IsNull([TemplateId], @TemplateId) = @TemplateId)
	Print FormatMessage ('Insert @Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Apply Changes
	Delete From [Obsolete].[TemplateNodeOwner]
	From	[Obsolete].[TemplateNodeOwner] T
			Left Join @Values S
			On	T.[NodeId] = S.[NodeId] And
				T.[NodeOwnerId] = S.[NodeOwnerId] And
				T.[TemplateId] = S.[TemplateId]
	Where	S.[NodeId] is Null And
			(@TemplateId is Not Null Or @ModelId is Not Null) And
			(@TemplateId is Null Or @TemplateId = T.[TemplateId])  And
			(@ModelId is Null Or T.[TemplateId] In (
				Select	[TemplateId]
				From	[Obsolete].[ScriptingModel]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppScript].[TemplateNodeOwner]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [Obsolete].[TemplateNodeOwner] (
			[NodeId],
			[NodeOwnerId],
			[TemplateId])
	Select	S.[NodeId],
			S.[NodeOwnerId],
			S.[TemplateId]
	From	@Values S
			Left Join [Obsolete].[TemplateNodeOwner] T
			On	T.[NodeId] = S.[NodeId] And
				T.[NodeOwnerId] = S.[NodeOwnerId] And
				T.[TemplateId] = S.[TemplateId]
	Where	T.[NodeId] is Null
	Print FormatMessage ('Insert [AppScript].[TemplateNodeOwner]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
