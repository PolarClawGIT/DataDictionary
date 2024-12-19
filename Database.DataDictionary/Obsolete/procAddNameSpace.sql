CREATE PROCEDURE [AppModel].[procAddNameSpace]
		@ModelId UniqueIdentifier = Null,
		@Data [AppModel].[typeNameSpace] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Add on NameSpace.
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
		[NameSpaceId]		UniqueIdentifier NOT NULL,
		[MemberName]		[App_DataDictionary].[typeNameSpaceMember] Not Null,
		[NameSpace]			[App_DataDictionary].[typeNameSpacePath] Null,
		[ParentNameSpace]	[App_DataDictionary].[typeNameSpacePath] Null,
		Primary Key ([NameSpaceId]))

	;With [Data] As (
		Select	Coalesce(S.[NameSpaceId], NewId()) As [NameSpaceId],
				N.[MemberName],
				N.[NameSpace],
				N.[ParentNameSpace],
				Row_Number() Over (Partition By N.[NameSpace] Order By N.[MemberName]) As [RankIndex]
		From	@Data D
				Cross Apply [AppModel].[funcSplitNameSpace](D.[NameSpace]) N
				Outer Apply (
					Select	[NameSpaceId]
					From	[AppModel].[funcGetNameSpaceByName](N.[NameSpace])
					Where	(@ModelId is Null and [ModelId] is Null) Or [ModelId] = @ModelId) S)
	Insert Into @Values
	Select	[NameSpaceId],
			[MemberName],
			[NameSpace],
			[ParentNameSpace]
	From	[Data] D
	Where	[RankIndex] = 1

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Insert Into [AppModel].[NameSpaceHierarchy] (
		[NameSpaceId],
		[ModelId],
		[ParentNameSpaceId],
		[MemberName])
	Select	S.[NameSpaceId],
			@ModelId As [ModelId],
			P.[NameSpaceId] As [ParentNameSpaceId],
			S.[MemberName]
	From	@Values S
			Left Join @Values P
			On	S.[ParentNameSpace] = P.[NameSpace]
			Left Join [AppModel].[NameSpaceHierarchy] T
			On	S.[NameSpaceId] = T.[NameSpaceId]
	Where	T.[NameSpaceId] is Null
	Print FormatMessage ('Insert [AppModel].[NameSpaceHierarchy]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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