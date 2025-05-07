CREATE PROCEDURE [AppModel].[procSetRelationshipAttribute]
		@ModelId UniqueIdentifier = Null,
		@RelationshipId UniqueIdentifier = Null,
		@Data [AppModel].[typeRelationshipAttribute] ReadOnly
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Set on Model RelationshipAttribute.
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
		[RelationshipId]			   UniqueIdentifier Not Null,
		[AttributeAliasId]     UniqueIdentifier Not Null,
		[AttributeKnownAs]	   [App_DataDictionary].[typeTitle] Not Null, -- What to call the Attribute within this Relationship (default is the Attribute Name)
		[OrdinalPosition]      Int Not Null,
		Primary Key ([RelationshipId], [AttributeAliasId]),
		Unique ([RelationshipId], [AttributeKnownAs]),
		Unique ([RelationshipId], [OrdinalPosition]))

	Declare @Alias [AppModel].[typeAlias];

	Insert Into @Alias ([AliasNameSpace])
	Select	[AttributePath]
	From	@Data

	Exec [AppModel].[procSetAlias] @ModelId = @ModelId, @Data = @Alias

	Insert Into @Values
	Select	D.[RelationshipId],
			[AppModel].[funcAliasId](D.[AttributePath]) As [AttributeAliasId],
			NullIf(Trim(D.[AttributeKnownAs]),'') As [AttributeKnownAs],
			D.[OrdinalPosition]
	From	@Data D
			Left Join [AppModel].[RelationshipAttributeHs] H
			On	D.[RelationshipId] = H.[RelationshipId] And
				D.[AttributePath] = H.[AttributePath]
	Where	(@RelationshipId is Null Or @RelationshipId = D.[RelationshipId]) And
			(@ModelId is Null Or D.[RelationshipId] In (
				Select	[RelationshipId]
				From	[AppModel].[ModelRelationship]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('@Values: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId

	-- Apply Changes
	Delete From [AppModel].[RelationshipAttribute]
	From	[AppModel].[RelationshipAttribute] T
			Left Join @Values V
			On	T.[RelationshipId] = V.[RelationshipId] And
				T.[AttributeAliasId] = V.[AttributeAliasId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, T.[RelationshipId], 1)
	Where	V.[AttributeAliasId] is Null And
			(@RelationshipId is Not Null Or @ModelId is Not Null) And
			(@RelationshipId is Null Or @RelationshipId = T.[RelationshipId])  And
			(@ModelId is Null Or T.[RelationshipId] In (
				Select	[RelationshipId]
				From	[AppModel].[ModelRelationship]
				Where	[ModelId] = @ModelId))
	Print FormatMessage ('Delete [AppModel].[RelationshipAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	;With [Delta] As (
		Select	[RelationshipId],
				[AttributeAliasId],
				[AttributeKnownAs],
				[OrdinalPosition]
		From	@Values
		Except
		Select	[RelationshipId],
				[AttributeAliasId],
				[AttributeKnownAs],
				[OrdinalPosition]
		From	[AppModel].[RelationshipAttribute])
	Update [AppModel].[RelationshipAttribute]
	Set		[AttributeKnownAs] = S.[AttributeKnownAs],
			[OrdinalPosition] = S.[OrdinalPosition]
	From	[AppModel].[RelationshipAttribute] T
			Inner Join [Delta] S
			On	T.[RelationshipId] = S.[RelationshipId] And
				T.[AttributeAliasId] = S.[AttributeAliasId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, S.[RelationshipId], 1)
	Print FormatMessage ('Update [AppModel].[RelationshipAttribute]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

	Insert Into [AppModel].[RelationshipAttribute] (
			[RelationshipId],
			[AttributeAliasId],
			[AttributeKnownAs],
			[OrdinalPosition])
	Select	S.[RelationshipId],
			S.[AttributeAliasId],
			S.[AttributeKnownAs],
			S.[OrdinalPosition]
	From	@Values S
			Left Join [AppModel].[RelationshipAttribute] T
			On	S.[RelationshipId] = T.[RelationshipId] And
				S.[AttributeAliasId] = T.[AttributeAliasId]
			Cross Apply [AppSecurity].[funcModelRelationshipAuthorization](@ModelId, S.[RelationshipId], 1)
	Where	T.[AttributeAliasId] is Null
	Print FormatMessage ('Insert [AppModel].[Relationship]: %i, %s',@@RowCount, Convert(VarChar,GetDate()));

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
