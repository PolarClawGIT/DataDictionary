CREATE PROCEDURE [App_DataDictionary].[procSetApplicationModel]
		@ModelId UniqueIdentifier = Null,
		@ModelTitle NVarChar(100) = Null,
		@ModelDescription NVarChar(1000) = Null,
		@Obsolete Bit = Null,
		@SysStart DateTime2 Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on ApplicationModel.
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
	   
	Select	@ModelTitle = NullIf(Trim(@ModelTitle),''),
			@ModelDescription = NullIf(Trim(@ModelDescription),''),
			@Obsolete = IsNull(@Obsolete,0)

	-- Validation
	if @ModelTitle is Null
	Throw 50000, '[ModelTitle] cannot be null or white-space',1;

	if Exists (Select 1 From [App_DataDictionary].[ApplicationModel] Where IsNull(@ModelId,'') <> [ModelId] And @ModelTitle = [ModelTitle] And [Obsolete] = 0)
	Throw 50000, '[ModelTitle] Can not be a duplicate, other then when Obsolete',2;

	if Exists (Select 1 From [App_DataDictionary].[ApplicationModel] Where @ModelId = [ModelId] And IsNull(@SysStart,[SysStart]) <> [SysStart])
	Throw 50000, '[SysStart] indicates that the Database Row may have changed since the source Row was originally extracted',3;

	-- Work
	With [Data] As (
		Select	@ModelId As [ModelId],
				@ModelTitle As [ModelTitle],
				@ModelDescription As [ModelDescription],
				IsNull(@Obsolete,0) As [Obsolete]),
	[Delta] As (
		Select	S.[ModelId],
				S.[ModelTitle],
				S.[ModelDescription],
				IIF(@Obsolete = 0, Convert(DateTime2, Null), IsNull([ObsoleteDate],SysDateTime())) As [ObsoleteDate]
		From	[Data] S
				Left Join [App_DataDictionary].[ApplicationModel] T
				On	S.[ModelId] = T.[ModelId]
		Except
		Select	[ModelId],
				[ModelTitle],
				[ModelDescription],
				[ObsoleteDate]
		From	[App_DataDictionary].[ApplicationModel])
	Merge [App_DataDictionary].[ApplicationModel] As T
	Using [Delta] As S
	On	T.[ModelId] = S.[ModelId]
	When Matched Then Update
	Set	[ModelTitle] = S.[ModelTitle],
		[ModelDescription] = S.[ModelDescription],
		[ObsoleteDate] = S.[ObsoleteDate]
	When Not Matched by Target Then
		Insert ([ModelId], [ModelTitle], [ModelDescription], [ObsoleteDate])
		Values ([ModelId], [ModelTitle], [ModelDescription], [ObsoleteDate]);

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
	Print FormatMessage (' @ModelId- %s',Convert(NVarChar,@ModelId))
	Print FormatMessage (' @ModelTitle- %s',Convert(NVarChar,@ModelTitle))
	Print FormatMessage (' @ModelDescription- %s',Convert(NVarChar,@ModelDescription))
	Print FormatMessage (' @Obsolete- %s',Convert(NVarChar,@Obsolete))
	Print FormatMessage (' @SysStart- %s',Convert(NVarChar,@SysStart))

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
-- Provide System Documentation
EXEC sp_addextendedproperty @name = N'MS_Description',
	@level0type = N'SCHEMA', @level0name = N'App_DataDictionary',
    @level1type = N'PROCEDURE', @level1name = N'procSetApplicationModel',
	@value = N'Performs Set on ApplicationModel.'
GO
