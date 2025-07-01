CREATE PROCEDURE [AppModel].[procSetAttribute]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@Data [AppModel].[udttAttribute] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Model Attribute.
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
				Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, [AttributeId], 0))
	Throw 601020, 'Model Not Authorized', 2;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[AttributeId]			UniqueIdentifier Not Null,
		[AttributeTitle]		[AppGeneral].[uddtTitle] Not Null,
		[AttributeDescription]	[AppGeneral].[uddtDescription] Null,
		[AttributeName]			[AppGeneral].[uddtQualifiedName] Null,
		[DataType]			    NVarChar(128) Null,
		[DataLength]		    SmallInt Null,
		[DataPrecision]		    TinyInt Null,
		[DataScale]				TinyInt Null,
		[IsSingleValue]			Bit Null,
		[IsSimpleType]			Bit Null,
		[IsIntegral]			Bit Null,
		[IsNullable]			Bit Null,
		[IsKey]					Bit Null,
		Primary Key ([AttributeId]))

	Insert Into @Values
	Select	X.[AttributeId],
			NullIf(Trim(D.[AttributeTitle]),'') As [AttributeTitle],
			NullIf(Trim(D.[AttributeDescription]),'') As [AttributeDescription],
			N.[AttributeName],
			NullIf(Trim(D.[DataType]),'') As [DataType],
			D.[DataLength],
			D.[DataPrecision],
			D.[DataScale],
			Case
				When D.[IsSingleValue] = 1 Then 1
				When D.[IsMultiValue] = 1 Then 0
				Else Null End As [IsSingleValue],
			Case
				When D.[IsSimpleType] = 1 Then 1
				When D.[IsCompositeType] = 1 Then 0
				Else Null End As [IsSimpleType],
			Case
				When D.[IsDerived] = 1 Then 0
				When D.[IsIntegral] = 1 Then 1
				Else Null End As [IsIntegral],
			Case
				When D.[IsNullable] = 1 Then 1
				When D.[IsValued] = 1 Then 0
				Else Null End As [IsNullable],
			Case
				When D.[IsKey] = 1 Then 1
				When D.[IsNonKey] = 1 Then 0
				Else Null End As [IsKey]
	From	@Data D
			Left Join [AppModel].[ModelAttributeHs] H
			On	(D.[AttributeId] = H.[AttributeId] Or
				 (H.[ModelId] = @ModelId And
				  D.[AttributeTitle] = H.[AttributeTitle]))
			Cross Apply (
				Select	Coalesce(D.[AttributeId], H.[AttributeId], NewId()) As [AttributeId]) X
			Outer Apply (
				Select	[QualifiedName] As [AttributeName]
				From	[AppGeneral].[funcParseName](D.[AttributeName])
				Where	[IsBase] = 1) N
	Where	(@ModelId is Null Or @ModelId = IsNull(H.[ModelId], @ModelId)) And
			(@AttributeId is Null Or @AttributeId = X.[AttributeId])
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Declare @Delete Table ([AttributeId] UniqueIdentifier Not Null)

	Insert Into @Delete
	Select	T.[AttributeId]
	From	[AppModel].[Attribute] T
			Left Join @Values S
			On	T.[AttributeId] = S.[AttributeId]
	Where	S.[AttributeId] is Null And
			(@AttributeId is Not Null Or @ModelId is Not Null) And
			(@AttributeId is Null Or @AttributeId = T.[AttributeId])  And
			(@ModelId is Null Or T.[AttributeId] In (
				Select	[AttributeId]
				From	[AppModel].[ModelAttribute]
				Group By [AttributeId]
				Having Sum(Case When [ModelId] = @ModelId Then 0 Else 1 End) = 0))

	Delete From [AppModel].[ModelAttribute]
	From	[AppModel].[ModelAttribute] T
			Left Join @Values S
			On	T.[AttributeId] = S.[AttributeId]
			Cross Apply [AppSecurity].[funcModelAttributeAuthorization](T.[ModelId], T.[AttributeId], 1)
	Where	S.[AttributeId] is Null And
			(@AttributeId is Not Null Or @ModelId is Not Null) And
			(@AttributeId is Null Or @AttributeId = T.[AttributeId])  And
			(@ModelId is Null Or @ModelId = T.[ModelId])
	Print FormatMessage ('Delete [AppModel].[ModelAttribute] (Attribute): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[AttributeAlias]
	From	[AppModel].[AttributeAlias] T
			Left Join @Values S
			On	T.[AttributeId] = S.[AttributeId]
			Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, T.[AttributeId], 1)
	Where	S.[AttributeId] is Null And
			T.[AttributeId] In (Select [AttributeId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[AttributeAlias] (Attribute): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[AttributeDefinition]
	From	[AppModel].[AttributeDefinition] T
			Left Join @Values S
			On	T.[AttributeId] = S.[AttributeId]
			Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, T.[AttributeId], 1)
	Where	S.[AttributeId] is Null And
			T.[AttributeId] In (Select [AttributeId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[AttributeDefinition] (Attribute): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[AttributeProperty]
	From	[AppModel].[AttributeProperty] T
			Left Join @Values S
			On	T.[AttributeId] = S.[AttributeId]
			Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, T.[AttributeId], 1)
	Where	S.[AttributeId] is Null And
			T.[AttributeId] In (Select [AttributeId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[AttributeProperty] (Attribute): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[AttributeSubjectArea]
	From	[AppModel].[AttributeSubjectArea] T
			Left Join @Values S
			On	T.[AttributeId] = S.[AttributeId]
			Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, T.[AttributeId], 1)
	Where	S.[AttributeId] is Null And
			T.[AttributeId] In (Select [AttributeId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[AttributeSubjectArea] (Attribute): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[Attribute]
	From	[AppModel].[Attribute] T
			Left Join @Values S
			On	T.[AttributeId] = S.[AttributeId]
			Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, T.[AttributeId], 1)
	Where	S.[AttributeId] is Null And
			T.[AttributeId] In (Select [AttributeId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[Attribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[AttributeId],
				[AttributeTitle],
				[AttributeDescription],
				[AttributeName],
				[DataType],
				[DataLength],
				[DataPrecision],
				[DataScale],
				[IsSingleValue],
				[IsSimpleType],
				[IsIntegral],
				[IsNullable],
				[IsKey]
		From	@Values
		Except
		Select	[AttributeId],
				[AttributeTitle],
				[AttributeDescription],
				[AttributeName],
				[DataType],
				[DataLength],
				[DataPrecision],
				[DataScale],
				[IsSingleValue],
				[IsSimpleType],
				[IsIntegral],
				[IsNullable],
				[IsKey]
		From	[AppModel].[Attribute])
	Update [AppModel].[Attribute]
	Set		[AttributeTitle] = S.[AttributeTitle],
			[AttributeDescription] = S.[AttributeDescription],
			[AttributeName] = S.[AttributeName],
			[DataType] = S.[DataType],
			[DataLength] = S.[DataLength],
			[DataScale] = S.[DataScale],
			[DataPrecision] = S.[DataPrecision],
			[IsSingleValue] = S.[IsSingleValue],
			[IsSimpleType] = S.[IsSimpleType],
			[IsIntegral] = S.[IsIntegral],
			[IsNullable] = S.[IsNullable],
			[IsKey] = S.[IsKey]
	From	[AppModel].[Attribute] T
			Inner Join [Delta] S
			On	T.[AttributeId] = S.[AttributeId]
			Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, S.[AttributeId], 1)
	Print FormatMessage ('Update [AppModel].[Attribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[Attribute] (
			[AttributeId],
			[AttributeTitle],
			[AttributeDescription],
			[AttributeName],
			[DataType],
			[DataLength],
			[DataPrecision],
			[DataScale],
			[IsSingleValue],
			[IsSimpleType],
			[IsIntegral],
			[IsNullable],
			[IsKey])
	Select	S.[AttributeId],
			S.[AttributeTitle],
			S.[AttributeDescription],
			S.[AttributeName],
			S.[DataType],
			S.[DataLength],
			S.[DataPrecision],
			S.[DataScale],
			S.[IsSingleValue],
			S.[IsSimpleType],
			S.[IsIntegral],
			S.[IsNullable],
			S.[IsKey]
	From	@Values S
			Left Join [AppModel].[Attribute] T
			On	S.[AttributeId] = T.[AttributeId]
			Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, S.[AttributeId], 1)
	Where	T.[AttributeId] is Null
	Print FormatMessage ('Insert [AppModel].[Attribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[ModelAttribute] (
			[ModelId],
			[AttributeId])
	Select	@ModelId As [ModelId],
			S.[AttributeId]
	From	@Values S
			Left Join [AppModel].[ModelAttribute] T
			On	S.[AttributeId] = T.[AttributeId] And
				@ModelId = T.[ModelId]
			Cross Apply [AppSecurity].[funcModelAttributeAuthorization](@ModelId, S.[AttributeId], 1)
	Where	T.[AttributeId] Is Null And
			@ModelId is Not Null
	Print FormatMessage ('Insert [AppModel].[ModelAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
