CREATE PROCEDURE [App_DataDictionary].[procSetDomainEntityAlias]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDomainEntityAlias] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DomainEntityAlias.
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

	-- Clean Data
	Declare @Values Table (
		[EntityId]       UniqueIdentifier Not Null,
		[AliasId]           UniqueIdentifier Not Null,
		[ScopeId]           Int Not Null,
		[AliasElement]		[App_DataDictionary].[typeAliasElement] Not Null,
		[AliasName]			[App_DataDictionary].[typeAliasName] Not Null,
		[ParentAliasName]	[App_DataDictionary].[typeAliasElement] Null
		Primary Key ([EntityId], [AliasId]))

	Declare @Alias Table (
		[AliasId]           UniqueIdentifier Not Null,
		[AliasElement]		[App_DataDictionary].[typeAliasElement] Not Null,
		[AliasName]			[App_DataDictionary].[typeAliasName] Not Null,
		[ParentAliasName]	[App_DataDictionary].[typeAliasElement] Null,
		Primary Key ([AliasId]))


	;With [Alias] As (
		Select	I.[AliasId],
				I.[AliasElement],
				F.[AliasName],
				F.[ParentAliasName]
		From	[App_DataDictionary].[DomainAlias] I
				Cross Apply [App_DataDictionary].[funcGetAliasName](I.[AliasId]) F),
	[Data] As (	
		Select	Coalesce(A.[AliasId],NewId()) As [AliasId],
				N.[AliasElement],
				N.[AliasName],
				N.[ParentAliasName],
				Row_Number() Over (
					Partition By A.[AliasId], N.[AliasName]
					Order By N.[AliasName])
					As [RankIndex]
		From	@Data D
				Cross Apply [App_DataDictionary].[funcSplitAliasName](D.[AliasName]) N
				Left Join [Alias] A
				On	N.[AliasName] = A.[AliasName])
	Insert Into @Alias
	Select	[AliasId],
			[AliasElement],
			[AliasName],
			[ParentAliasName]
	From	[Data]
	Where	[RankIndex] = 1

	;With [Scope] As (
		Select	S.[ScopeId],
				F.[ScopeName]
		From	[App_DataDictionary].[ApplicationScope] S
				Cross Apply [App_DataDictionary].[funcGetScopeName](S.[ScopeId]) F)
	Insert Into @Values
	Select	Coalesce(D.[EntityId], @EntityId) As [EntityId],
			A.[AliasId],
			S.[ScopeId],
			A.[AliasElement],
			A.[AliasName],
			A.[ParentAliasName]
	From	@Data D
			Left Join [Scope] S
			On	D.[ScopeName] = S.[ScopeName]
			Left Join @Alias A
			On	D.[AliasName] = A.[AliasName]

	-- Apply Changes
	Insert Into [App_DataDictionary].[DomainAlias] ([AliasId], [ParentAliasId], [AliasElement])
	Select	V.[AliasId],
			P.[AliasId] As [ParentAliasId],
			V.[AliasElement]
	From	@Alias V
			Left Join @Alias P
			On	V.[ParentAliasName] = P.[AliasName]
			Left Join [App_DataDictionary].[DomainAlias] T
			On	V.[AliasId] = T.[AliasId]
	Where	T.[AliasId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DomainAlias] (Entity): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DomainEntityAlias]
	From	[App_DataDictionary].[DomainEntityAlias] T
			Left Join @Values V
			On	T.[EntityId] = V.[EntityId] And
				T.[AliasId] = V.[AliasId]
	Where	V.[EntityId] is Null And
			T.[EntityId] In (
			Select	A.[EntityId]
			From	[App_DataDictionary].[DomainEntity] A
					Left Join [App_DataDictionary].[ModelEntity] C
					On	A.[EntityId] = C.[EntityId]
			Where	(@EntityId is Null Or @EntityId = A.[EntityId]) And
					(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DomainEntityAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[EntityId],
				[AliasId],
				[ScopeId]
		From	@Values
		Except
		Select	[EntityId],
				[AliasId],
				[ScopeId]
		From	[App_DataDictionary].[DomainEntityAlias])
	Update [App_DataDictionary].[DomainEntityAlias]
	Set		[ScopeId] = S.[ScopeId]
	From	[App_DataDictionary].[DomainEntityAlias] T
			Inner Join [Delta] S
			On	T.[EntityId] = S.[EntityId] And
				T.[AliasId] = S.[AliasId]
	Print FormatMessage ('Update [App_DataDictionary].[DomainEntityAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DomainEntityAlias] ([EntityId], [AliasId], [ScopeId])
	Select	V.[EntityId],
			V.[AliasId],
			V.[ScopeId]
	From	@Values V
			Left Join [App_DataDictionary].[DomainEntityAlias] T
			On	V.[EntityId] = T.[EntityId] And
				V.[AliasId] = T.[AliasId]
	Where	T.[EntityId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DomainEntityAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
