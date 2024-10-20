﻿CREATE PROCEDURE [App_DataDictionary].[procSetDomainAttribute]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@Data [App_DataDictionary].[typeDomainAttribute] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on DomainAttribute.
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
	If @ModelId is Null and @AttributeId is Null
	Throw 50000, '@ModelId or @AttributeId must be specified', 1;

	-- Clean the Data, helps performance
	Declare @Values Table (
		    [AttributeId]          UniqueIdentifier Not Null,
			[AttributeTitle]       [App_DataDictionary].[typeTitle] Not Null,
			[AttributeDescription] [App_DataDictionary].[typeDescription] Null,
			[MemberName]           [App_DataDictionary].[typeNameSpaceMember] NULL,
			[IsSingleValue]        Bit Null,
			[IsSimpleType]         Bit Null,
			[IsDerived]            Bit Null,
			[IsNullable]           Bit Null,
			[IsKey]                Bit Null,
		Primary Key ([AttributeId]))

	Declare @Delete Table (
		[AttributeId] UniqueIdentifier Not Null,
		Primary Key ([AttributeId]))

	Insert Into @Values
	Select	X.[AttributeId],
			NullIf(Trim(D.[AttributeTitle]),'') As [AttributeTitle],
			NullIf(Trim(D.[AttributeDescription]),'') As [AttributeDescription],
			M.[NameSpace] As [MemberName],
			Case
				When D.[IsSingleValue] = 1 Then 1
				When D.[IsMultiValue] = 1 Then 0
				Else Null End As [IsSingleValue],
			Case
				When D.[IsSimpleType] = 1 Then 1
				When D.[IsCompositeType] = 1 Then 0
				Else Null End As [IsSimpleType],
			Case
				When D.[IsDerived] = 1 Then 1
				When D.[IsIntegral] = 1 Then 0
				Else Null End As [IsDerived],
			Case
				When D.[IsNullable] = 1 Then 1
				When D.[IsValued] = 1 Then 0
				Else Null End As [IsNullable],
			Case
				When D.[IsKey] = 1 Then 1
				When D.[IsNonKey] = 1 Then 0
				Else Null End As [IsKey]
	From	@Data D
			Outer Apply [App_DataDictionary].[funcSplitNameSpace](IsNull(D.[MemberName], D.[AttributeTitle])) M
			Cross apply (
				Select	Coalesce(D.[AttributeId], @AttributeId, NewId()) As [AttributeId]) X
	Where	M.[IsBase] = 1

	Insert Into @Delete
	Select	T.[AttributeId]
	From	[App_DataDictionary].[DomainAttribute] T
			Left Join @Values S
			On	T.[AttributeId] = S.[AttributeId]
	Where	S.[AttributeId] is Null And
			T.[AttributeId] In (
			Select	A.[AttributeId]
			From	[App_DataDictionary].[DomainAttribute] A
					Left Join [App_DataDictionary].[ModelAttribute] C
					On	A.[AttributeId] = C.[AttributeId]
			Where	(@AttributeId is Null Or @AttributeId = A.[AttributeId]) And
					(@ModelId is Null Or @ModelId = C.[ModelId]))

	-- Apply Changes
	Delete From [App_DataDictionary].[DomainEntityAttribute]
	From	[App_DataDictionary].[DomainEntityAttribute] T
			Inner Join @Delete S
			On	T.[AttributeId] = S.[AttributeId]
	Print FormatMessage ('Delete [App_DataDictionary].[DomainEntityAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DomainAttributeProperty]
	From	[App_DataDictionary].[DomainAttributeProperty] T
			Inner Join @Delete S
			On	T.[AttributeId] = S.[AttributeId]
	Print FormatMessage ('Delete [App_DataDictionary].[DomainAttributeProperty]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DomainAttributeAlias]
	From	[App_DataDictionary].[DomainAttributeAlias] T
			Inner Join @Delete S
			On	T.[AttributeId] = S.[AttributeId]
	Print FormatMessage ('Delete [App_DataDictionary].[DomainAttributeAlias]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[ModelAttribute]
	From	[App_DataDictionary].[ModelAttribute] T
			Inner Join @Delete S
			On	T.[AttributeId] = S.[AttributeId]
	Print FormatMessage ('Delete [App_DataDictionary].[ModelAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Delete From [App_DataDictionary].[DomainAttribute]
	From	[App_DataDictionary].[DomainAttribute] T
			Inner Join @Delete S
			On	T.[AttributeId] = S.[AttributeId]
	Print FormatMessage ('Delete [App_DataDictionary].[DomainAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[AttributeId],
				[AttributeTitle],
				[AttributeDescription],
				[IsSingleValue],
				[IsSimpleType],
				[IsDerived],
				[IsNullable],
				[IsKey]
		From	@Values
		Except
		Select	[AttributeId],
				[AttributeTitle],
				[AttributeDescription],
				[IsSingleValue],
				[IsSimpleType],
				[IsDerived],
				[IsNullable],
				[IsKey]
		From	[App_DataDictionary].[DomainAttribute])
	Update [App_DataDictionary].[DomainAttribute]
	Set		[AttributeTitle] = S.[AttributeTitle],
			[AttributeDescription] = S.[AttributeDescription]
	From	[App_DataDictionary].[DomainAttribute] T
			Inner Join [Delta] S
			On	T.[AttributeId] = S.[AttributeId]
	Print FormatMessage ('Update [App_DataDictionary].[DomainAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	@ModelId [ModelId],
				[AttributeId],
				[MemberName]
		From	@Values
		Except
		Select	[ModelId],
				[AttributeId],
				[MemberName]
		From	[App_DataDictionary].[ModelAttribute])
	Update [App_DataDictionary].[ModelAttribute]
	Set		[MemberName] = S.[MemberName]
	From	[App_DataDictionary].[ModelAttribute] T
			Inner Join [Delta] S
			On	T.[ModelId] = S.[ModelId] And
				T.[AttributeId] = S.[AttributeId]
	Print FormatMessage ('Update [App_DataDictionary].[ModelAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[DomainAttribute] (
			[AttributeId],
			[AttributeTitle],
			[AttributeDescription],
			[IsSingleValue],
			[IsSimpleType],
			[IsDerived],
			[IsNullable],
			[IsKey])
	Select	S.[AttributeId],
			S.[AttributeTitle],
			S.[AttributeDescription],
			S.[IsSingleValue],
			S.[IsSimpleType],
			S.[IsDerived],
			S.[IsNullable],
			S.[IsKey]
	From	@Values S
			Left Join [App_DataDictionary].[DomainAttribute] T
			On	S.[AttributeId] = T.[AttributeId]
	Where	T.[AttributeId] is Null
	Print FormatMessage ('Insert [App_DataDictionary].[DomainAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [App_DataDictionary].[ModelAttribute] (
			[ModelId],
			[AttributeId],
			[MemberName])
	Select	@ModelId As [ModelId],
			S.[AttributeId],
			S.[MemberName]
	From	@Values S
			Left Join [App_DataDictionary].[ModelAttribute] T
			On	S.[AttributeId] = T.[AttributeId] And
				@ModelId = T.[ModelId]
	Where	T.[AttributeId] Is Null
	Print FormatMessage ('Insert [App_DataDictionary].[ModelAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
