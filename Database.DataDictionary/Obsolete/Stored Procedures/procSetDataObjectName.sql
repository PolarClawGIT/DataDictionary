CREATE PROCEDURE [Obsolete].[procSetDataObjectName]
		@ModelId UniqueIdentifier = Null,
		@DataSourceId UniqueIdentifier = Null,
		@Data [Obsolete].[udttDataObjectName] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ObjectName.
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
		From	@Data D
				Cross Apply [AppSecurity].[funcScriptingAuthorization]([DataSourceId], 0))
	Throw 601020, 'DataSource Not Authorized', 2;

	Declare @Values Table (
		[DataSourceId]			UniqueIdentifier Not Null,
		[ObjectNameId]			UniqueIdentifier Not Null,
		[ObjectNameMember]		[AppGeneral].[uddtMember] Not Null,
		[ParentNameId]			UniqueIdentifier Null,
		-- Temporary
		[ObjectNameSpace]	[AppGeneral].[uddtPath] Not Null,
		[ParentNameSpace]	[AppGeneral].[uddtPath] Null,
		Primary Key ([ObjectNameId]))

	;With [Data] As (
		Select	IsNull([DataSourceId], @DataSourceId) As [DataSourceId],
				X.[ObjectNameId],
				P.[MemberName] As [ObjectNameMember],
				P.[QualifiedName] As [ObjectNameSpace],
				P.[ParentName] As [ParentNameSpace],
				Row_Number() Over (Partition By [QualifiedName] Order By IIF(X.[ObjectNameId] is not null,0,1)) As [RankIndex]
		From	@Data D
				Cross Apply [AppGeneral].[funcParseName](D.[ObjectPath]) P
				Cross Apply (
					Select	IsNull([Obsolete].[funcObjectNameId]([QualifiedName]), NewId()) As [ObjectNameId]) X)
	Insert Into @Values
	Select	[DataSourceId],
			Coalesce([Obsolete].[funcObjectNameId]([ObjectNameSpace]), [ObjectNameId], NewId()) As [ObjectNameId],
			[ObjectNameMember],
			Null,
			[ObjectNameSpace],
			[ParentNameSpace]
	From	[Data] D
	Where	[RankIndex] = 1 And
			(@DataSourceId is Null Or @DataSourceId = [DataSourceId])
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Insert Into [Obsolete].[DataObjectName] (
			[DataSourceId],
			[ObjectNameId],
			[ObjectMember],
			[ParentNameId])
	Select	S.[DataSourceId],
			S.[ObjectNameId],
			S.[ObjectNameMember],
			P.[ObjectNameId] As [ParentNameId]
	From	@Values S
			Left Join @Values P
			On	S.[ParentNameSpace] = P.[ObjectNameSpace]
			Left Join [Obsolete].[DataObjectName] T
			On	S.[ObjectNameId] = T.[ObjectNameId]
			Cross Apply [AppSecurity].[funcModelAuthorization](@ModelId, 1)
	Where	T.[ObjectNameId] is Null
	Print FormatMessage ('Insert [AppScript].[DataObjectName]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
