CREATE PROCEDURE [AppScript].[procSetDataObject]
		@ModelId UniqueIdentifier = Null,
		@DataSourceId UniqueIdentifier = Null,
		@Data [AppScript].[udttDataObject] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DataObject.
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

	If Exists (
		Select	1
		From	@Data D
				Cross Apply [AppSecurity].[funcScriptingAuthorization]([DataSourceId], 0))
	Throw 601020, 'DataSource Not Authorized', 2;

	Declare @Values Table (
		[DataObjectId]		UniqueIdentifier Not Null,
		[DataSourceId]		UniqueIdentifier Not Null,
		[ObjectNameId]		UniqueIdentifier Not Null,
		[ObjectScope]		[AppGeneral].[uddtScopeName] NOT NULL,
		Primary Key ([DataObjectId]))

	Declare @ObjectName [AppScript].[udttDataObjectName]
		
	Insert Into @ObjectName ([DataSourceId], [ObjectPath])
	Select	[DataSourceId],
			[ObjectPath]
	From	@Data

	Exec [AppScript].[procSetDataObjectName] @ModelId = @ModelId, @DataSourceId = @DataSourceId, @Data = @ObjectName
			
	Insert Into @Values
	Select	X.[DataObjectId],
			IsNull(D.[DataSourceId], @DataSourceId) As [DataSourceId],
			N.[ObjectNameId],
			D.[ObjectScope]
	From	@Data D
			Cross Apply (Select [AppScript].[funcObjectNameId]([ObjectPath]) As [ObjectNameId]) N
			Left Join [AppScript].[DataObject] O
			On	IsNull(D.[DataSourceId], @DataSourceId) = O.[DataSourceId] And
				N.[ObjectNameId] = O.[ObjectNameId] And
				D.[ObjectScope] = O.[ObjectScope]
			Cross Apply (
				Select	Coalesce(O.[DataObjectId], NewId()) As [DataObjectId]) X
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
		
	-- Apply Changes
	;With [Data] As (
		Select	D.[DataSourceId],
				N.[ObjectNameId],
				N.[ParentNameId]
		From	@Values D
				Inner Join [AppScript].[DataObjectName] N
				On	D.[ObjectNameId] = N.[ObjectNameId]
		Union All
		Select	D.[DataSourceId],
				N.[ObjectNameId],
				N.[ParentNameId]
		From	[Data] D
				Inner Join [AppScript].[DataObjectName] N
				On	D.[ObjectNameId] = N.[ParentNameId])
		Delete From [AppScript].[DataObjectName]
		From	[AppScript].[DataObjectName] T
				Left Join [Data] S
				On	T.[ObjectNameId] = S.[ObjectNameId]
				Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[DataSourceId], 1)
		Where	S.[ObjectNameId] is Null And
				(@DataSourceId is Not Null Or @ModelId is Not Null) And
				(@DataSourceId is Null Or @DataSourceId = T.[DataSourceId])  And
				(@ModelId is Null Or T.[DataSourceId] In (
					Select	[DataSourceId]
					From	[AppScript].[ScriptingModel]
					Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppScript].[DataObjectName]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppScript].[DataObject]
	From	[AppScript].[DataObject] T
			Left Join @Values S
			On	T.[DataObjectId] = S.[DataObjectId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](T.[DataSourceId], 1)
	Where	S.[DataObjectId] is Null And
			(@DataSourceId is Not Null Or @ModelId is Not Null) And
			(@DataSourceId is Null Or @DataSourceId = T.[DataSourceId])  And
			(@ModelId is Null Or T.[DataSourceId] In (
				Select	[DataSourceId]
				From	[AppScript].[ScriptingModel]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppScript].[DataObject]: %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppScript].[DataObject] (
			[DataObjectId],
			[DataSourceId],
			[ObjectNameId],
			[ObjectScope])
	Select	S.[DataObjectId],
			S.[DataSourceId],
			S.[ObjectNameId],
			S.[ObjectScope]
	From	@Values S
			Left Join [AppScript].[DataObject] T
			On	S.[DataObjectId] = T.[DataObjectId]
			Cross Apply [AppSecurity].[funcScriptingAuthorization](S.[DataSourceId], 1)
	Where	T.[DataObjectId] is Null
	Print FormatMessage ('Insert [AppScript].[DataObject]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
