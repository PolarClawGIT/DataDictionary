CREATE PROCEDURE [App_DataDictionary].[procSetModel]
		@ModelId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeModel] ReadOnly
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

	-- Clean the Data
	Declare @Values [App_DataDictionary].[typeModel]
	Insert Into @Values
	Select	V.[ModelId],
			NullIf(Trim([ModelTitle]),'') As [ModelTitle],
			NullIf(Trim([ModelDescription]),'') As [ModelDescription],
			[Obsolete],
			[SysStart]
	From	@Data D
			Outer Apply (Select	IsNull([ModelId],NewId()) As [ModelId]) V
	Where	(@ModelId is Null or @ModelId = V.[ModelId])

	-- Validation
	If Exists (
		Select	[ModelTitle]
		From	@Values
		Group By [ModelTitle]
		Having	Count(*) > 1)
	Throw 50000, '[ModelTitle] cannot be duplicate', 2;

	If Exists ( -- Set [SysStart] to Null in parameter data to bypass this check
		Select	D.[ModelId]
		From	@Values D
				Inner Join [App_DataDictionary].[Model] A
				On D.[ModelId] = A.[ModelId]
		Where	IsNull(D.[SysStart],A.[SysStart]) <> A.[SysStart])
	Throw 50000, '[SysStart] indicates that the Database Row may have changed since the source Row was originally extracted', 4;

	-- Apply Changes
	With [Delta] As (
		Select	V.[ModelId],
				V.[ModelTitle],
				V.[ModelDescription],
				IIF(IsNull(V.[Obsolete], A.[Obsolete]) = 0, Convert(DateTime2, Null), IsNull(A.[ObsoleteDate],SysDateTime())) As [ObsoleteDate]
		From	@Values V
				Left Join [App_DataDictionary].[Model] A
				On	V.[ModelId] = A.[ModelId]
		Except
		Select	[ModelId],
				[ModelTitle],
				[ModelDescription],
				[ObsoleteDate]
		From	[App_DataDictionary].[Model])
	Merge [App_DataDictionary].[Model] As T
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
    @level1type = N'PROCEDURE', @level1name = N'procSetModel',
	@value = N'Performs Set on ApplicationModel.'
GO