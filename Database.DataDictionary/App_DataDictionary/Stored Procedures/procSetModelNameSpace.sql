--DROP PROCEDURE If Exists [App_DataDictionary].[procSetModelNameSpace]
--GO
CREATE PROCEDURE [App_DataDictionary].[procSetModelNameSpace]
		@ModelId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeNameSpace] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ModelNameSpace.
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

	-- Clean the Data
	Declare @Values Table (
		[NameSpaceId]            UniqueIdentifier NOT NULL,
		[MemberName]		 [App_DataDictionary].[typeNameSpaceMember] Not Null,
		[NameSpace]              [App_DataDictionary].[typeNameSpacePath] Null,
		[ParentNameSpace]        [App_DataDictionary].[typeNameSpacePath] Null,
		Primary Key ([NameSpaceId]))

	;With [NameSpace] As (
		Select	S.[NameSpaceId],
				S.[MemberName],
				N.[NameSpace]
		From	[App_DataDictionary].[ModelNameSpace] S
				Cross Apply [App_DataDictionary].[funcGetNameSpace](S.[NameSpaceId]) N
		Where	@ModelId is Null Or S.[ModelId] = @ModelId),
		[Data] As (
			Select	X.[NameSpaceId],
					S.[MemberName],
					S.[NameSpace],
					S.[ParentNameSpace],
					S.[Level] As [NameSpaceLevel],
					ROW_NUMBER() Over (
						Partition By S.[NameSpace]
						Order By S.[NameSpace] Desc) As [RankIndex]
			From	@Data D
					Cross Apply [App_DataDictionary].[funcSplitNameSpace](D.[NameSpace]) S
					Left Join [NameSpace] N
					On	S.[NameSpace] = N.[NameSpace]
					Cross apply (
						Select	Coalesce(N.[NameSpaceId], D.[NameSpaceId], NewId()) As [NameSpaceId]) X
					)
		Insert Into @Values
		Select	[NameSpaceId],
				[MemberName],
				[NameSpace],
				[ParentNameSpace]
		From	[Data]
		Where	[RankIndex] = 1

	-- Apply Changes
	Insert Into [App_DataDictionary].[ModelNameSpace] (
			[NameSpaceId],
			[ModelId],
			[ParentNameSpaceId],
			[MemberName])
	Select	V.[NameSpaceId],
			@ModelId,
			P.[NameSpaceId] As [ParentNameSpaceId],
			V.[MemberName]
	From	@Values V
			Left Join @Values P
			On	V.[ParentNameSpace] = P.[NameSpace]
			Left Join [App_DataDictionary].[ModelNameSpace] M
			On	V.[NameSpaceId] = M.[NameSpaceId]
	Where	M.[NameSpaceId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ModelNameSpace]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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

	Declare	@ModelId UniqueIdentifier = (Select Min([ModelId]) From [App_DataDictionary].[Model]),
			@Data [App_DataDictionary].[typeNameSpace]

	Insert Into @Data Values
		(Null, '[Test]'),
		(Null, '[Test].[SubTest]'),
		(Null, '[Test].[OtherTest]')

	Exec [App_DataDictionary].[procSetModelNameSpace] @ModelId, @Data

	Select	S.[NameSpaceId],
				S.[MemberName],
				N.[NameSpace]
		From	[App_DataDictionary].[ModelNameSpace] S
				Cross Apply [App_DataDictionary].[funcGetNameSpace](S.[NameSpaceId]) N

	Insert Into @Data Values
		(Null, '[Test]'),
		(Null, '[Test].[Sub.Test]'),
		(Null, '[Test].[OtherTest]')

	Exec [App_DataDictionary].[procSetModelNameSpace] @ModelId, @Data

	Select	S.[NameSpaceId],
				S.[MemberName],
				N.[NameSpace]
		From	[App_DataDictionary].[ModelNameSpace] S
				Cross Apply [App_DataDictionary].[funcGetNameSpace](S.[NameSpaceId]) N

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
	Throw;
End Catch;
*/