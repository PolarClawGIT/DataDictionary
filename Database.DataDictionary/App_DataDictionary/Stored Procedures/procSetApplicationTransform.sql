CREATE PROCEDURE [App_DataDictionary].[procSetApplicationTransform]
		@TransformId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeApplicationTransform] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ApplicationTransform.
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

	-- Clean the Data, helps performance
	Declare @Values Table (
			[TransformId]             UniqueIdentifier NOT NULL,
			[TransformTitle]          [App_DataDictionary].[typeTitle] Not Null,
			[TransformDescription]    [App_DataDictionary].[typeDescription] Null,
			[ScopeId]                 Int Not Null,
			[AsText]                  Bit Null,
			[TransformScript]         XML Null,
			Primary Key ([TransformId]))

	;With [Scope] As (
		Select	S.[ScopeId],
				F.[ScopeName]
		From	[App_DataDictionary].[ApplicationScope] S
				Cross Apply [App_DataDictionary].[funcGetScopeName](S.[ScopeId]) F)
	Insert Into @Values
	Select	X.[TransformId],
			NullIf(Trim(D.[TransformTitle]),'') As [TransformTitle],
			NullIf(Trim(D.[TransformDescription]),'') As [TransformDescription],
			S.[ScopeId],
			Case
				When D.[AsText] = 1 Then 1
				When D.[AsXml] = 1 Then 0
				Else Null End As [AsText],
			D.[TransformScript]
	From	@Data D
			Left Join [Scope] S
				On	D.[ScopeName] = S.[ScopeName]
			Cross apply (
				Select	Coalesce(D.[TransformId], @TransformId, NewId()) As [TransformId]) X
	Where	(@TransformId is Null or D.[TransformId] = @TransformId)

	-- Apply Changes
	Delete From [App_DataDictionary].[ApplicationTransform]
	From	[App_DataDictionary].[ApplicationTransform] T
			Left Join @Values S
			On	T.[TransformId] = S.[TransformId]
	Where	S.[TransformId] is Null And
			(@TransformId is Null or T.[TransformId] = @TransformId)
	Print FormatMessage ('Delete [App_DataDictionary].[DatabaseTable]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[TransformId],
				[TransformTitle],
				[TransformDescription],
				[ScopeId],
				[AsText],
				Convert(NVarChar(Max),[TransformScript]) As [TransformScript]
		From	@Values
		Except
		Select	[TransformId],
				[TransformTitle],
				[TransformDescription],
				[ScopeId],
				[AsText],
				Convert(NVarChar(Max),[TransformScript]) As [TransformScript]
		From	[App_DataDictionary].[ApplicationTransform])
	Update [App_DataDictionary].[ApplicationTransform]
	Set		[TransformTitle] = S.[TransformTitle],
			[TransformDescription] = S.[TransformDescription],
			[ScopeId] = S.[ScopeId],
			[AsText] = S.[AsText],
			[TransformScript] = S.[TransformScript]
	From	[App_DataDictionary].[ApplicationTransform] T
			Inner Join [Delta] S
			On	T.[TransformId] = S.[TransformId]
	Print FormatMessage ('Update [App_DataDictionary].[ApplicationTransform]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [TransformTitle] (
			[TransformId],
			[TransformTitle],
			[TransformDescription],
			[ScopeId],
			[AsText],
			[TransformScript])
	Select	S.[TransformId],
			S.[TransformTitle],
			S.[TransformDescription],
			S.[ScopeId],
			S.[AsText],
			S.[TransformScript]
	From	@Values S
			Left Join [App_DataDictionary].[ApplicationTransform] T
			On	S.[TransformId] = T.[TransformId]
	Where	T.[TransformId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ApplicationTransform]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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