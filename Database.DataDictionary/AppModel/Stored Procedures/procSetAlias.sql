CREATE PROCEDURE [AppModel].[procSetAlias]
		@ModelId UniqueIdentifier = Null,
		@Data [AppModel].[udttAlias] ReadOnly
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

	-- Validation
	If Exists (
		Select	1
		From	[AppSecurity].[funcModelAuthorization](@ModelId, 0))
	Throw 601020, 'Model Not Authorized', 2;

	Declare @Values Table (
		[AliasId]			UniqueIdentifier Not Null,
		[AliasMember]		NVarChar(800) Not Null,
		[ParentAliasId]		UniqueIdentifier Null,
		-- Temporary
		[AliasNameSpace]	[AppGeneral].[uddtNameSpacePath] Not Null,
		[ParentNameSpace]	[AppGeneral].[uddtNameSpacePath] Null,
		Primary Key ([AliasId]))

	;With [Data] As (
		Select	[AliasId],
				[MemberName] As [AliasMember],
				[QualifiedName] As [AliasNameSpace],
				[ParentName] As [ParentNameSpace],
				Row_Number() Over (Partition By [QualifiedName] Order By IIF([AliasId] is not null,0,1)) As [RankIndex]
		From	@Data D
				Cross Apply [AppGeneral].[funcParseName](D.[AliasNameSpace]))
	Insert Into @Values
	Select	Coalesce([AppModel].[funcAliasId]([AliasNameSpace]), [AliasId], NewId()) As [AliasId],
			[AliasMember],
			Null,
			[AliasNameSpace],
			[ParentNameSpace]
	From	[Data] D
	Where	[RankIndex] = 1
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
		
	-- Apply Changes
	Insert Into [AppModel].[AliasHierarchy] (
			[AliasId],
			[AliasMember],
			[ParentAliasId])
	Select	S.[AliasId],
			S.[AliasMember],
			P.[AliasId] As [ParentAliasId]
	From	@Values S
			Left Join @Values P
			On	S.[ParentNameSpace] = P.[AliasNameSpace]
			Left Join [AppModel].[AliasHierarchy] T
			On	S.[AliasId] = T.[AliasId]
			Cross Apply [AppSecurity].[funcModelAuthorization](@ModelId, 1)
	Where	T.[AliasId] is Null
	Print FormatMessage ('Insert [AppModel].[AliasHierarchy]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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