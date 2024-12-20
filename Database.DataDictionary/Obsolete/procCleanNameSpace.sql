CREATE PROCEDURE [AppModel].[procCleanNameSpace]
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Clean out unused (orphaned) NameSpace.
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

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	;With [Data] As (
		-- Alias
		Select	[NameSpaceId]
		From	[App_DataDictionary].[DomainEntityAlias]
		Union
		Select	[NameSpaceId]
		From	[App_DataDictionary].[DomainProcessAlias]
		Union
		Select	[NameSpaceId]
		From	[App_DataDictionary].[DomainRelationshipAlias]
		Union
		Select	[NameSpaceId]
		From	[AppModel].[EntitySubjectArea]
		Union
		Select	[NameSpaceId]
		From	[AppModel].[ProcessSubjectArea]
		Union
		Select	[NameSpaceId]
		From	[AppModel].[RelationshipSubjectArea]
		-- Scripting (may be re-factored)
		Union
		Select	[NameSpaceId]
		From	[App_DataDictionary].[ScriptingPath]),
	[Parents] As (
		Select	U.[NameSpaceId],
				P.[ParentNameSpaceId]
		From	[Data] U
				Inner Join [AppModel].[NameSpaceHierarchy] P
				On	U.[NameSpaceId] = P.[NameSpaceId]
		Union All
		Select	P.[NameSpaceId],
				P.[ParentNameSpaceId]
		From	[Parents] U
				Inner Join [AppModel].[NameSpaceHierarchy] P
				On	U.[ParentNameSpaceId] = P.[NameSpaceId]),
	[Delete] As (
		Select	[NameSpaceId]
		From	[AppModel].[NameSpaceHierarchy]
		Except
		Select	[NameSpaceId]
		From	[Parents])
	Delete From [AppModel].[NameSpaceHierarchy]
	From	[AppModel].[NameSpaceHierarchy] T
			Inner Join [Delete] S
			On	T.[NameSpaceId] = S.[NameSpaceId]
	Print FormatMessage ('Delete [AppModel].[NameSpaceHierarchy]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
	-- Rollback Transaction
	If @TRN_IsNewTran = 1
	  Begin -- If this is the outer transaction, roll it back
		Rollback Transaction
		Print FormatMessage ('Rollback Transaction Issued ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))
	  End -- Rollback Transaction
	-- This is a nested transaction, must be rolled back by outer transaction
	Else Print FormatMessage ('Rollback Transaction Pending ([%s].[%s])', Object_Schema_Name(@@ProcID),Object_Name(@@ProcID))

	If ERROR_NUMBER() >= 50000 Exec [AppGeneral].[procThrowHelpSubject]
	Else If ERROR_SEVERITY() Not In (0, 11) Throw;
End Catch
GO