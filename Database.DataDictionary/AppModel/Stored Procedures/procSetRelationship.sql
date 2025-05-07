CREATE PROCEDURE [AppModel].[procSetRelationship]
		@ModelId UniqueIdentifier = Null,
		@RelationshipId UniqueIdentifier = Null,
		@Data [AppModel].[typeRelationship] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Model Relationship.
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
				Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, [RelationshipId], 0))
	Throw 601020, 'Model Not Authorized', 2;

	-- Clean the Data, helps performance
	Declare @Values Table (
		[RelationshipId]			    UniqueIdentifier Not Null,
		[RelationshipTitle]		    [App_DataDictionary].[typeTitle] Not Null,
		[RelationshipDescription]	[App_DataDictionary].[typeDescription] Null,
		[RelationshipName]			[AppModel].[typeQualifiedName] Null,
		[RelationshipType]			NVarChar(20) Not Null, -- Type of Relationship
		[OwnerAliasId]				UniqueIdentifier Not Null, -- Owner of the Relationship. Normally an Entity.
		[RefrenceAliasId]			UniqueIdentifier Null, -- Relationship referenced, if any. (FK's and Compound, normally)
		Primary Key ([RelationshipId]),
		Unique ([RelationshipTitle]))

	Declare @Alias [AppModel].[typeAlias];

	Insert Into @Alias ([AliasNameSpace])
	Select	[OwnerAliasPath]
	From	@Data
	Where	[OwnerAliasPath] is not null
	Union
	Select	[RefrenceAliasPath]
	From	@Data
	Where	[OwnerAliasPath] is not null

	Exec [AppModel].[procSetAlias] @ModelId = @ModelId, @Data = @Alias

	Insert Into @Values
	Select	Coalesce(D.[RelationshipId], H.[RelationshipId], NewId()) As [RelationshipId],
			NullIf(Trim(D.[RelationshipTitle]),'') As [RelationshipTitle],
			NullIf(Trim(D.[RelationshipDescription]),'') As [RelationshipDescription],
			N.[RelationshipName],
			D.[RelationshipType],
			[AppModel].[funcAliasId] (D.[OwnerAliasPath]) As [OwnerAliasId],
			[AppModel].[funcAliasId] (D.[RefrenceAliasPath]) As [RefrenceAliasId]
	From	@Data D
			Left Join [AppModel].[ModelRelationshipHs] H
			On	(D.[RelationshipId] = H.[RelationshipId] Or
				 (H.[ModelId] = @ModelId And
				  D.[RelationshipTitle] = H.[RelationshipTitle]))
			Cross Apply (
				Select	Coalesce(D.[RelationshipId], H.[RelationshipId], NewId()) As [RelationshipId]) X
			Outer Apply (
				Select	[QualifiedName] As [RelationshipName]
				From	[AppModel].[funcParseName](D.[RelationshipName])
				Where	[IsBase] = 1) N
	Where	(@ModelId is Null Or @ModelId = IsNull(H.[ModelId], @ModelId)) And
			(@RelationshipId is Null Or @RelationshipId = X.[RelationshipId])
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Declare @Delete Table ([RelationshipId] UniqueIdentifier Not Null)

	Insert Into @Delete
	Select	T.[RelationshipId]
	From	[AppModel].[Relationship] T
			Left Join @Values S
			On	T.[RelationshipId] = S.[RelationshipId]
	Where	S.[RelationshipId] is Null And
			(@RelationshipId is Not Null Or @ModelId is Not Null) And
			(@RelationshipId is Null Or @RelationshipId = T.[RelationshipId])  And
			(@ModelId is Null Or T.[RelationshipId] In (
				Select	[RelationshipId]
				From	[AppModel].[ModelRelationship]
				Group By [RelationshipId]
				Having Sum(Case When [ModelId] = @ModelId Then 0 Else 1 End) = 0))

	Delete From [AppModel].[ModelRelationship]
	From	[AppModel].[ModelRelationship] T
			Left Join @Values S
			On	T.[RelationshipId] = S.[RelationshipId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](T.[ModelId], T.[RelationshipId], 1)
	Where	S.[RelationshipId] is Null And
			(@RelationshipId is Not Null Or @ModelId is Not Null) And
			(@RelationshipId is Null Or @RelationshipId = T.[RelationshipId])  And
			(@ModelId is Null Or @ModelId = T.[ModelId])
	Print FormatMessage ('Delete [AppModel].[ModelRelationship] (Relationship): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[RelationshipAlias]
	From	[AppModel].[RelationshipAlias] T
			Left Join @Values S
			On	T.[RelationshipId] = S.[RelationshipId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, T.[RelationshipId], 1)
	Where	S.[RelationshipId] is Null And
			T.[RelationshipId] In (Select [RelationshipId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[RelationshipAlias] (Relationship): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[RelationshipDefinition]
	From	[AppModel].[RelationshipDefinition] T
			Left Join @Values S
			On	T.[RelationshipId] = S.[RelationshipId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, T.[RelationshipId], 1)
	Where	S.[RelationshipId] is Null And
			T.[RelationshipId] In (Select [RelationshipId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[RelationshipDefinition] (Relationship): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[RelationshipProperty]
	From	[AppModel].[RelationshipProperty] T
			Left Join @Values S
			On	T.[RelationshipId] = S.[RelationshipId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, T.[RelationshipId], 1)
	Where	S.[RelationshipId] is Null And
			T.[RelationshipId] In (Select [RelationshipId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[RelationshipProperty] (Relationship): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[RelationshipSubjectArea]
	From	[AppModel].[RelationshipSubjectArea] T
			Left Join @Values S
			On	T.[RelationshipId] = S.[RelationshipId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, T.[RelationshipId], 1)
	Where	S.[RelationshipId] is Null And
			T.[RelationshipId] In (Select [RelationshipId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[RelationshipSubjectArea] (Relationship): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[RelationshipAttribute]
	From	[AppModel].[RelationshipAttribute] T
			Left Join @Values S
			On	T.[RelationshipId] = S.[RelationshipId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, T.[RelationshipId], 1)
	Where	S.[RelationshipId] is Null And
			T.[RelationshipId] In (Select [RelationshipId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[RelationshipAttribute] (Relationship): %i, %s', @@RowCount, Convert(VarChar,GetDate()));

	Delete From [AppModel].[Relationship]
	From	[AppModel].[Relationship] T
			Left Join @Values S
			On	T.[RelationshipId] = S.[RelationshipId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, T.[RelationshipId], 1)
	Where	S.[RelationshipId] is Null And
			T.[RelationshipId] In (Select [RelationshipId] From @Delete)
	Print FormatMessage ('Delete [AppModel].[Relationship]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[RelationshipId],
				[RelationshipTitle],
				[RelationshipDescription],
				[RelationshipName],
				[RelationshipType],
				[OwnerAliasId],
				[RefrenceAliasId]
		From	@Values
		Except
		Select	[RelationshipId],
				[RelationshipTitle],
				[RelationshipDescription],
				[RelationshipName],
				[RelationshipType],
				[OwnerAliasId],
				[RefrenceAliasId]
		From	[AppModel].[Relationship])
	Update [AppModel].[Relationship]
	Set		[RelationshipTitle] = S.[RelationshipTitle],
			[RelationshipDescription] = S.[RelationshipDescription],
			[RelationshipName] = S.[RelationshipName],
			[RelationshipType] = S.[RelationshipType],
			[OwnerAliasId] = S.[OwnerAliasId],
			[RefrenceAliasId] = S.[RefrenceAliasId]
	From	[AppModel].[Relationship] T
			Inner Join [Delta] S
			On	T.[RelationshipId] = S.[RelationshipId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, S.[RelationshipId], 1)
	Print FormatMessage ('Update [AppModel].[Relationship]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[Relationship] (
			[RelationshipId],
			[RelationshipTitle],
			[RelationshipDescription],
			[RelationshipName],
			[RelationshipType],
			[OwnerAliasId],
			[RefrenceAliasId])
	Select	S.[RelationshipId],
			S.[RelationshipTitle],
			S.[RelationshipDescription],
			S.[RelationshipName],
			S.[RelationshipType],
			S.[OwnerAliasId],
			S.[RefrenceAliasId]
	From	@Values S
			Left Join [AppModel].[Relationship] T
			On	S.[RelationshipId] = T.[RelationshipId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, S.[RelationshipId], 1)
	Where	T.[RelationshipId] is Null
	Print FormatMessage ('Insert [AppModel].[Relationship]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[ModelRelationship] (
			[ModelId],
			[RelationshipId])
	Select	@ModelId As [ModelId],
			S.[RelationshipId]
	From	@Values S
			Left Join [AppModel].[ModelRelationship] T
			On	S.[RelationshipId] = T.[RelationshipId] And
				@ModelId = T.[ModelId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, S.[RelationshipId], 1)
	Where	T.[RelationshipId] Is Null And
			@ModelId is Not Null
	Print FormatMessage ('Insert [AppModel].[ModelRelationship]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
