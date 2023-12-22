﻿CREATE PROCEDURE [App_DataDictionary].[procSetDomainAttributeAlias]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDomainAttributeAlias] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DomainAttributeAlias.
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
		[AttributeId]          UniqueIdentifier Not Null,
		[AliasId]           UniqueIdentifier Not Null,
		[ScopeId]           Int Not Null,
		[AliasElement]		[App_DataDictionary].[typeAliasElement] Not Null,
		[AliasName]			[App_DataDictionary].[typeAliasName] Not Null,
		[ParentAliasName]	[App_DataDictionary].[typeAliasElement] Null,
		[IsBase]            Bit Not Null,
		Primary Key ([AttributeId], [AliasId] ))

	;With [Scope] As (
		Select	S.[ScopeId],
				F.[ScopeName]
		From	[App_DataDictionary].[ApplicationScope] S
				Cross Apply [App_DataDictionary].[funcGetScopeName](S.[ScopeId]) F),
	[Alias] As (
		Select	I.[AliasId],
				F.[AliasName]
		From	[App_DataDictionary].[DomainAlias] I
				Cross Apply [App_DataDictionary].[funcGetAliasName](I.[AliasId]) F)
	Insert Into @Values
	Select	Coalesce(D.[AttributeId], @AttributeId) As [AttributeId],
			Coalesce(A.[AliasId], NewId()) As [AliasId],
			S.[ScopeId],
			N.[AliasElement],
			N.[AliasName],
			N.[ParentAliasName],
			N.[IsBase]
	From	@Data D
			Cross Apply [App_DataDictionary].[funcSplitAliasName](D.[AliasName]) N
			Left Join [Scope] S
			On	D.[ScopeName] = S.[ScopeName]
			Left Join [Alias] A
			On	N.[AliasName] = A.[AliasName]

	-- Apply Changes
	Insert Into [App_DataDictionary].[DomainAlias] ([AliasId], [ParentAliasId], [AliasElement])
	Select	V.[AliasId],
			P.[AliasId] As [ParentAliasId],
			V.[AliasElement]
	From	@Values V
			Left Join @Values P
			On	V.[ParentAliasName] = P.[AliasName]
			Left Join [App_DataDictionary].[DomainAlias] T
			On	V.[AliasId] = T.[AliasId]
	Where	T.[AliasId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DomainAlias] (Attribute): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DomainAttributeAlias]
	From	[App_DataDictionary].[DomainAttributeAlias] T
			Left Join @Values V
			On	T.[AttributeId] = V.[AttributeId] And
				T.[AliasId] = V.[AliasId]
	Where	V.[AttributeId] is Null And
			T.[AttributeId] In (
			Select	A.[AttributeId]
			From	[App_DataDictionary].[DomainAttribute] A
					Left Join [App_DataDictionary].[ModelAttribute] C
					On	A.[AttributeId] = C.[AttributeId]
			Where	(@AttributeId is Null Or @AttributeId = A.[AttributeId]) And
					(@ModelId is Null Or @ModelId = C.[ModelId]))
	Print FormatMessage ('Delete [App_DataDictionary].[DomainAttributeAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[AttributeId],
				[AliasId],
				[ScopeId]
		From	@Values
		Where	[IsBase] = 1
		Except
		Select	[AttributeId],
				[AliasId],
				[ScopeId]
		From	[App_DataDictionary].[DomainAttributeAlias])
	Update [App_DataDictionary].[DomainAttributeAlias]
	Set		[ScopeId] = S.[ScopeId]
	From	[App_DataDictionary].[DomainAttributeAlias] T
			Inner Join [Delta] S
			On	T.[AttributeId] = S.[AttributeId] And
				T.[AliasId] = S.[AliasId]
	Print FormatMessage ('Update [App_DataDictionary].[DomainAttributeAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DomainAttributeAlias] ([AttributeId], [AliasId], [ScopeId])
	Select	V.[AttributeId],
			V.[AliasId],
			V.[ScopeId]
	From	@Values V
			Left Join [App_DataDictionary].[DomainAttributeAlias] T
			On	V.[AttributeId] = T.[AttributeId] And
				V.[AliasId] = T.[AliasId]
	Where	T.[AttributeId] is Null And
			V.[IsBase] = 1
	Print FormatMessage ('Insert [App_DataDictionary].[DomainAttributeAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
