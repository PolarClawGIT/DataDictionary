CREATE PROCEDURE [App_DataDictionary].[procSetDomainEntity]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDomainEntity] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DomainEntity.
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
	If @ModelId is Null and @EntityId is Null
	Throw 50000, '@ModelId or @EntityId must be specified', 1;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[EntityId] UniqueIdentifier Not Null,
		[EntityTitle] [App_DataDictionary].[typeTitle] Not Null,
		[EntityDescription] [App_DataDictionary].[typeDescription] Null,
		[MemberName] [App_DataDictionary].[typeNameSpaceMember] NULL,
		[TypeOfEntityId] UniqueIdentifier Null,
		Primary Key ([EntityId]))

	Declare @Delete Table (
		[EntityId] UniqueIdentifier Not Null,
		Primary Key ([EntityId]))

	Insert Into @Values
	Select	Coalesce(T.[EntityId], D.[EntityId], NewId()) As [EntityId],
			NullIf(Trim(D.[EntityTitle]),'') As [EntityTitle],
			NullIf(Trim(D.[EntityDescription]),'') As [EntityDescription],
			M.[NameSpace] As [MemberName],
			D.[TypeOfEntityId]
	From	@Data D
			Left Join [App_DataDictionary].[DomainEntity] T
			On	Coalesce(D.[EntityId], @EntityId) = T.[EntityId]
			Outer Apply [App_DataDictionary].[funcSplitNameSpace](IsNull(D.[MemberName], D.[EntityTitle])) M

	Insert Into @Delete
	Select	T.[EntityId]
	From	[App_DataDictionary].[DomainEntity] T
			Left Join @Values S
			On	T.[EntityId] = S.[EntityId]
	Where	S.[EntityId] is Null And
			T.[EntityId] In (
			Select	A.[EntityId]
			From	[App_DataDictionary].[DomainEntity] A
					Left Join [App_DataDictionary].[ModelEntity] C
					On	A.[EntityId] = C.[EntityId]
			Where	(@EntityId is Null Or @EntityId = A.[EntityId]) And
					(@ModelId is Null Or @ModelId = C.[ModelId]))

	-- Apply Changes
	Delete From [App_DataDictionary].[DomainEntityProperty]
	From	[App_DataDictionary].[DomainEntityProperty] T
			Inner Join @Delete S
			On	T.[EntityId] = S.[EntityId]
	Print FormatMessage ('Delete [App_DataDictionary].[DomainEntityProperty] (Entity): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DomainEntityAlias]
	From	[App_DataDictionary].[DomainEntityAlias] T
			Inner Join @Delete S
			On	T.[EntityId] = S.[EntityId]
	Print FormatMessage ('Delete [App_DataDictionary].[DomainEntityAlias] (Entity): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[ModelEntity]
	From	[App_DataDictionary].[ModelEntity] T
			Inner Join @Delete S
			On	T.[EntityId] = S.[EntityId]
	Print FormatMessage ('Delete [App_DataDictionary].[ModelEntity] (Entity): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DomainEntity]
	From	[App_DataDictionary].[DomainEntity] T
			Inner Join @Delete S
			On	T.[EntityId] = S.[EntityId]
	Print FormatMessage ('Delete [App_DataDictionary].[DomainEntity] (Entity): %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[EntityId],
				[EntityTitle],
				[EntityDescription],
				[TypeOfEntityId]
		From	@Values
		Except
		Select	[EntityId],
				[EntityTitle],
				[EntityDescription],
				[TypeOfEntityId]
		From	[App_DataDictionary].[DomainEntity])
	Update [App_DataDictionary].[DomainEntity]
	Set		[EntityTitle] = S.[EntityTitle],
			[EntityDescription] = S.[EntityDescription],
			[TypeOfEntityId] = S.[TypeOfEntityId]
	From	[App_DataDictionary].[DomainEntity] T
			Inner Join [Delta] S
			On	T.[EntityId] = S.[EntityId]
	Print FormatMessage ('Update [App_DataDictionary].[DomainEntity]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	@ModelId [ModelId],
				[EntityId],
				[MemberName]
		From	@Values
		Except
		Select	[ModelId],
				[EntityId],
				[MemberName]
		From	[App_DataDictionary].[ModelEntity])
	Update [App_DataDictionary].[ModelEntity]
	Set		[MemberName] = S.[MemberName]
	From	[App_DataDictionary].[ModelEntity] T
			Inner Join [Delta] S
			On	T.[ModelId] = S.[ModelId] And
				T.[EntityId] = S.[EntityId]
	Print FormatMessage ('Update [App_DataDictionary].[ModelEntity]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DomainEntity] (
			[EntityId],
			[EntityTitle],
			[EntityDescription],
			[TypeOfEntityId])
	Select	S.[EntityId],
			S.[EntityTitle],
			S.[EntityDescription],
			S.[TypeOfEntityId]
	From	@Values S
			Left Join [App_DataDictionary].[DomainEntity] T
			On	S.[EntityId] = T.[EntityId]
	Where	T.[EntityId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DomainEntity]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ModelEntity] (
			[ModelId],
			[EntityId],
			[MemberName])
	Select	@ModelId As [ModelId],
			S.[EntityId],
			S.[MemberName]
	From	@Values S
			Left Join [App_DataDictionary].[ModelEntity] T
			On	S.[EntityId] = T.[EntityId] And
				@ModelId = T.[ModelId]
	Where	T.[EntityId] Is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ModelEntity]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
	Print FormatMessage (' @ModelId- %s',Convert(NVarChar(50),@ModelId))

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
