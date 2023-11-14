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

	-- Insert any missing Alias Refrences
	Declare @Alias [App_DataDictionary].[typeAlias]	
	Insert Into @Alias
	Select	[SourceName],
			[AliasName]
	From	@Data
	Group By [SourceName],
			[AliasName]

	Exec [App_DataDictionary].[procInsertAlias] @Alias

	-- Clean Data
	Declare @Values Table (
		[AliasId]           UniqueIdentifier Not Null,
		[ScopeId]           Int NOT Null,
		[EntityId]          UniqueIdentifier Not Null, 
		Primary Key ([EntityId], [AliasId] ))

	;With [Scope] As (
		Select	S.[ScopeId],
				F.[ScopeName]
		From	[App_DataDictionary].[AliasScope] S
				Cross Apply [App_DataDictionary].[funcGetScopeName](S.[ScopeId]) F),
	[Alias] As (
		Select	I.[AliasId],
				S.[SourceName],
				F.[AliasName]
		From	[App_DataDictionary].[AliasItem] I
				Inner Join [App_DataDictionary].[AliasSource] S
				On	I.[AliasSourceId] = S.[AliasSourceId]
				Cross Apply [App_DataDictionary].[funcGetAliasName](I.[AliasId]) F)
	Insert Into @Values
	Select	A.[AliasId],
			S.[ScopeId],
			IsNull(D.[EntityId],@EntityId) As [EntityId]
	From	@Data D
			Cross Apply [App_DataDictionary].[funcSplitAliasName](D.[AliasName]) N
			Left Join [Scope] S
			On	D.[ScopeName] = S.[ScopeName]
			Left Join [Alias] A
			On	D.[SourceName] = A.[SourceName] And
				N.[AliasName] = A.[AliasName]
	Where	N.[IsBase] = 1

	-- Apply Changes
	Declare @Deleted Table ([AliasId] UniqueIdentifier Not Null)
	
	Delete From [App_DataDictionary].[AliasDomain]
	Output [deleted].[AliasId] Into @Deleted
	From	[App_DataDictionary].[AliasDomain] T
			Left Join @Values V
			On	T.[AliasId] = V.[AliasId] And
				T.[EntityId] = V.[EntityId]
			Left Join [App_DataDictionary].[AliasItem] I
			On	T.[AliasId] = I.[ParentAliasId]
	Where	V.[AliasId] is Null And
			I.[AliasId] is Null And
			T.[EntityId] In (
				Select	A.[EntityId]
				From	[App_DataDictionary].[DomainEntity] A
						Left Join [App_DataDictionary].[ModelEntity] M
						On	A.[EntityId] = M.[EntityId]
				Where	(@EntityId is Null Or @EntityId = A.[EntityId]) And
						(@ModelId is Null Or @ModelId = M.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[AliasDomain] (Entity): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[AliasItem]
	From	@Deleted D
			Inner Join [App_DataDictionary].[AliasItem] T
			On	D.[AliasId] = T.[AliasId]
			Left Join [App_DataDictionary].[AliasDomain] C
			On	D.[AliasId] = C.[AliasId]
	Where	C.[AliasId] is Null
	Print FormatMessage ('Delete [App_DataDictionary].[AliasItem] (Entity): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[AliasDomain] (
			[AliasId],
			[ScopeId],
			[EntityId])
	Select	V.[AliasId],
			V.[ScopeId],
			V.[EntityId]
	From	@Values V
			Left Join [App_DataDictionary].[AliasDomain] T
			On	V.[AliasId] = T.[AliasId]
	Where	T.[AliasId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[AliasDomain] (Entity): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
