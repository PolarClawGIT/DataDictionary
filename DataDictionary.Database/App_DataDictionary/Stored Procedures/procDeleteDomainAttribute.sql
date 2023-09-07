﻿CREATE PROCEDURE [App_DataDictionary].[procDeleteDomainAttribute]
		@ModelId UniqueIdentifier = null,
		@AttributeId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Delete on DomainAttribute.
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
	If Not Exists (Select 1 From [App_DataDictionary].[DomainAttribute] Where [AttributeId] = @AttributeId And [Obsolete] = 1)
	Throw 50000, '[AttributeId] could not be found or is not marked as [Obsolete]', 1;

	Declare @Delete Table (
		[AttributeId] UniqueIdentifier Not Null)

	Insert Into @Delete
	Select	M.[AttributeId]
	From	[App_DataDictionary].[ModelAttribute] M
			Left Join [App_DataDictionary].[ModelAttribute] R
			On	M.[AttributeId] = R.[AttributeId] And
				R.[ModelId] <> @ModelId
	Where	(@AttributeId is Null or M.[AttributeId] = @AttributeId) And
			(@ModelId is Null or M.[ModelId] = @ModelId) And
			(@AttributeId is Not Null Or @ModelId is Not Null) And
			R.[ModelId] is Null

	-- Cascade Delete
	Delete From [App_DataDictionary].[DomainAttributeAlias]
	Where [AttributeId] In (Select [AttributeId] From @Delete)

	Delete From [App_DataDictionary].[DomainAttributeProperty]
	Where [AttributeId] In (Select [AttributeId] From @Delete)

	Delete From [App_DataDictionary].[ModelAttribute]
	Where [AttributeId] In (Select [AttributeId] From @Delete)

	Delete From [App_DataDictionary].[DomainAttribute]
	Where [AttributeId] In (Select [AttributeId] From @Delete)

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
	Print FormatMessage (' @AttributeId- %s',Convert(NVarChar(50),@AttributeId))

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
    @level1type = N'PROCEDURE', @level1name = N'procDeleteDomainAttribute',
	@value = N'Performs Delete on DomainAttribute.'
GO