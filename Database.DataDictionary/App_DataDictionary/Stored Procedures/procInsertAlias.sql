CREATE PROCEDURE [App_DataDictionary].[procInsertAlias]
		@Data [App_DataDictionary].[typeAlias] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Alias.
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

	Declare @Alias Table (
		[AliasId] UniqueIdentifier Not Null,
		[AliasName] [App_DataDictionary].[typeAliasName] Not Null,
		[SourceName] NVarChar(128) Not Null,
		[IsCaseSensitive] Bit Not Null,
		Primary Key ([SourceName], [AliasId]))

	Declare @Values Table (
		[AliasId] UniqueIdentifier Not Null,
		[SourceName] NVarChar(128) Not Null,
		[AliasName] [App_DataDictionary].[typeAliasName] Not Null,
		[ParentAliasName] [App_DataDictionary].[typeAliasName] Null,
		[AliasElement] [App_DataDictionary].[typeAliasElement] Not Null,
		Primary Key ([SourceName], [AliasId]))

	Insert Into @Alias
	Select	I.[AliasId],
			F.[AliasName],
			S.[SourceName],
			S.[IsCaseSensitive]
	From	[App_DataDictionary].[AliasItem] I
			Inner Join [App_DataDictionary].[AliasSource] S
			On	I.[AliasSourceId] = S.[AliasSourceId]
			Cross Apply [App_DataDictionary].[funcGetAliasName](I.[AliasId]) F
	Print FormatMessage ('Insert @Alias: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Values] As (
		Select	Distinct
				D.[SourceName],
				P.[AliasName],
				P.[ParentAliasName],
				P.[AliasElement]
		From	@Data D
				Cross Apply [App_DataDictionary].[funcSplitAliasName] (D.[AliasName]) P)
	Insert Into @Values
	Select	IsNull(A.[AliasId], NewId()) As [AliasId],
			V.[SourceName],
			V.[AliasName],
			V.[ParentAliasName],
			V.[AliasElement]
	From	[Values] V
			Left Join @Alias A
			On	V.[AliasName] = A.[AliasName] And
				V.[SourceName] = A.[SourceName]
	Print FormatMessage ('Insert @Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[AliasItem] (
			[AliasId],
			[ParentAliasId],
			[AliasSourceId],
			[AliasElement])
	Select	V.[AliasId],
			IsNull(A.[AliasId],P.[AliasId]) As [ParentAliasId],
			S.[AliasSourceId],
			V.[AliasElement]
	From	@Values V
			Left Join @Values P
			On	V.[SourceName] = P.[SourceName] And
				V.[ParentAliasName] = P.[AliasName]
			Left Join @Alias A
			On	V.[SourceName] = A.[SourceName] And
				V.[ParentAliasName] = A.[AliasName]
			Left Join [App_DataDictionary].[AliasSource] S
			On	V.[SourceName] = S.[SourceName]
			Left Join [App_DataDictionary].[AliasItem] T
			On	V.[AliasId] = T.[AliasId]
	Where	T.[AliasId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[Alias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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