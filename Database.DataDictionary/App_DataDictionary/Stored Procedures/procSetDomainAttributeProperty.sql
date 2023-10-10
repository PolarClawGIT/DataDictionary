CREATE PROCEDURE [App_DataDictionary].[procSetDomainAttributeProperty]
		@ModelId UniqueIdentifier,
		@Data [App_DataDictionary].[typeDomainAttributeProperty] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DomainAttributeProperty.
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
	Declare @Values [App_DataDictionary].[typeDomainAttributeProperty]
	Insert Into @Values
	Select	D.[AttributeId],
			P.[PropertyId],
			NullIf(Trim(D.[PropertyValue]),'') As [PropertyValue],
			NullIf(Trim(D.[DefinitionText]),'') As [DefinitionText],
			D.[SysStart]
	From	@Data D
			Inner Join [App_DataDictionary].[ApplicationProperty] P
			On	D.[PropertyId] = P.[PropertyId]

	-- Validation
	If Not Exists (Select 1 From [App_DataDictionary].[Model] Where [ModelId] = @ModelId)
	Throw 50000, '[ModelId] could not be found that matched the parameter', 1;

	If Exists (
		Select	V.[AttributeId]
		From	@Values V
				Left Join [App_DataDictionary].[DomainAttribute] A
				On	V.[AttributeId] = A.[AttributeId]
				Left Join [App_DataDictionary].[ModelAttribute] P
				On	V.[AttributeId] = P.[AttributeId] And
					P.[ModelId] = @ModelId
		Where	A.[AttributeId] is Null Or
				P.[AttributeId] is Null)
	Throw 50000, '[AttributeId] could not be found or is not associated with Model specified', 2;

	If Exists ( -- Set [SysStart] to Null in parameter data to bypass this check
		Select	D.[AttributeId]
		From	@Values D
				Inner Join [App_DataDictionary].[DomainAttributeProperty] A
				On D.[AttributeId] = A.[AttributeId] And
					D.[PropertyId] = A.[PropertyId]
		Where	IsNull(D.[SysStart],A.[SysStart]) <> A.[SysStart])
	Throw 50000, '[SysStart] indicates that the Database Row may have changed since the source Row was originally extracted', 4;

	-- Apply Changes
	With [Delta] As (
		Select	[AttributeId],
				[PropertyId],
				[PropertyValue],
				[DefinitionText]
		From	@Values
		Except
		Select	[AttributeId],
				[PropertyId],
				[PropertyValue],
				[DefinitionText]
		From	[App_DataDictionary].[DomainAttributeProperty]),
	[Data] As (
		Select	V.[AttributeId],
				V.[PropertyId],
				V.[PropertyValue],
				V.[DefinitionText],
				IIF(D.[AttributeId] is Null,0, 1) As [IsDiffrent]
		From	@Values V
				Left Join [Delta] D
				On	V.[AttributeId] = D.[AttributeId] and
					V.[PropertyId] = D.[PropertyId])
	Merge [App_DataDictionary].[DomainAttributeProperty] T
	Using [Data] S
	On	T.[AttributeId] = S.[AttributeId] And
		T.[PropertyId] = S.[PropertyId]
	When Matched And S.[IsDiffrent] = 1 Then Update
		Set	[PropertyValue] = S.[PropertyValue],
			[DefinitionText] = S.[DefinitionText]
	When Not Matched by Target Then
		Insert ([AttributeId], [PropertyId], [PropertyValue], [DefinitionText])
		Values ([AttributeId], [PropertyId], [PropertyValue], [DefinitionText])
	When Not Matched by Source And (T.[AttributeId] in (
		Select	[AttributeId]
		From	[App_DataDictionary].[ModelAttribute]
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
-- Provide System Documentation
EXEC sp_addextendedproperty @name = N'MS_Description',
	@level0type = N'SCHEMA', @level0name = N'App_DataDictionary',
    @level1type = N'PROCEDURE', @level1name = N'procSetDomainAttributeProperty',
	@value = N'Performs Set on DomainAttributeProperty.'
GO
