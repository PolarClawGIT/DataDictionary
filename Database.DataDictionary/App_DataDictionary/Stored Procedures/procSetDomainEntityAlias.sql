CREATE PROCEDURE [App_DataDictionary].[procSetDomainEntityAlias]
		@ModelId UniqueIdentifier,
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

	-- Clean the Data
	Declare @Values [App_DataDictionary].[typeDomainEntityAlias]
	Insert Into @Values
	Select	D.[EntityId],
			IsNull(C.[EntityAliasId],
				(Select IsNull(Max([EntityAliasId]),0) From [App_DataDictionary].[DomainEntityAlias] Where [EntityId] = D.[EntityId]) +
				Row_Number() Over (
					Partition By D.[EntityId], C.[EntityAliasId]
					Order By D.[DatabaseName], D.[SchemaName], D.[ObjectName]))
				As [EntityAliasId],
			NullIf(Trim(D.[DatabaseName]),'') As [DatabaseName],
			NullIf(Trim(D.[SchemaName]),'') As [SchemaName],
			NullIf(Trim(D.[ObjectName]),'') As [ObjectName],
			D.[SysStart]
	From	@Data D
			Left Join [App_DataDictionary].[DomainEntityAlias] C
			On	D.[EntityId] = C.[EntityId] And
				D.[DatabaseName] = C.[DatabaseName] And
				D.[SchemaName] = C.[SchemaName] And
				D.[ObjectName] = C.[ObjectName]

	-- Validation
	If Not Exists (Select 1 From [App_DataDictionary].[Model] Where [ModelId] = @ModelId)
	Throw 50000, '[ModelId] could not be found that matched the parameter', 1;

	If Exists (
		Select	V.[EntityId]
		From	@Values V
				Left Join [App_DataDictionary].[DomainEntity] A
				On	V.[EntityId] = A.[EntityId]
				Left Join [App_DataDictionary].[ModelEntity] P
				On	V.[EntityId] = P.[EntityId] And
					P.[ModelId] = @ModelId
		Where	A.[EntityId] is Null Or
				P.[EntityId] is Null)
	Throw 50000, '[EntityId] could not be found or is not associated with Model specified', 2;

	If Exists (
		Select	[DatabaseName],
				[SchemaName],
				[ObjectName]
		From	@Values
		Group By [DatabaseName],
				[SchemaName],
				[ObjectName]
		Having Count(*) > 1)
		Throw 50000, '[EntityId] Aliases can only be associated with a single Entity', 3;

	If Exists ( -- Set [SysStart] to Null in parameter data to bypass this check
		Select	D.[EntityId]
		From	@Values D
				Inner Join [App_DataDictionary].[DomainEntityAlias] A
				On D.[EntityId] = A.[EntityId] And
					D.[DatabaseName] = A.[DatabaseName] And
					D.[SchemaName] = A.[SchemaName] And
					D.[ObjectName] = A.[ObjectName]
		Where	IsNull(D.[SysStart],A.[SysStart]) <> A.[SysStart])
	Throw 50000, '[SysStart] indicates that the Database Row may have changed since the source Row was originally extracted', 4;

	-- Apply Changes
	With [Data] As (
		Select	D.[EntityId],
				D.[EntityAliasId],
				@ModelId As [ModelId],
				D.[DatabaseName],
				D.[SchemaName],
				D.[ObjectName]
		From	@Values D
				Left Join [App_DataDictionary].[DomainEntityAlias] A
				On	D.[EntityId] = A.[EntityId] And
					D.[DatabaseName] = A.[DatabaseName] And
					D.[SchemaName] = A.[SchemaName] And
					D.[ObjectName] = A.[ObjectName])
	Merge [App_DataDictionary].[DomainEntityAlias] T
	Using [Data] S
	On	T.[EntityId] = S.[EntityId] And
		T.[EntityAliasId] = S.[EntityAliasId]
	When Not Matched by Target Then
		Insert ([EntityId], [EntityAliasId], [ModelId], [DatabaseName], [SchemaName], [ObjectName])
		Values ([EntityId], [EntityAliasId], [ModelId], [DatabaseName], [SchemaName], [ObjectName])
	When Not Matched by Source And (T.[EntityId] in (
		Select	[EntityId]
		From	[App_DataDictionary].[ModelEntity]
		Where	[ModelId] = @ModelId))
		Then Delete;

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