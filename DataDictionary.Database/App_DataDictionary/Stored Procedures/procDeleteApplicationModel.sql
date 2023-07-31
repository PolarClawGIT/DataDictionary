CREATE PROCEDURE [App_DataDictionary].[procDeleteApplicationModel]
		@ModelId UniqueIdentifier
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Delete on ApplicationModel.
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
	If Not Exists (Select 1 From [App_DataDictionary].[ApplicationModel] Where [ModelId] = @ModelId And [Obsolete] = 1)
	Throw 50000, '[ModelId] could not be found or is not marked as [Obsolete]', 1;

	-- Get List of items that can be deleted (Not in multiple Models)
	Declare @Catalog Table ([CatalogId] UniqueIdentifier Not Null Primary Key)
	Declare @Attribute Table ([AttributeId] UniqueIdentifier Not Null Primary key)
	Declare @Entity Table ([EntityId] UniqueIdentifier Not Null Primary key)

	Insert Into @Catalog
	Select	[CatalogId]
	From	[App_DataDictionary].[ApplicationCatalog]
	Where	[ModelId] = @ModelId And
			[CatalogId] Not In (
				Select	[CatalogId]
				From	[App_DataDictionary].[ApplicationCatalog]
				Where	[ModelId] <> @ModelId)

	Insert Into @Attribute
	Select	[AttributeId]
	From	[App_DataDictionary].[ApplicationAttribute]
	Where	[ModelId] = @ModelId And
			[AttributeId] Not In (
				Select	[AttributeId]
				From	[App_DataDictionary].[ApplicationAttribute]
				Where	[ModelId] <> @ModelId)

	Insert Into @Entity
	Select	[EntityId]
	From	[App_DataDictionary].[ApplicationEntity]
	Where	[ModelId] = @ModelId And
			[EntityId] Not In (
				Select	[EntityId]
				From	[App_DataDictionary].[ApplicationEntity]
				Where	[ModelId] <> @ModelId)

	-- Cascade Delete
	Delete From [App_DataDictionary].[DatabaseExtendedProperty]
	Where	[CatalogId] In (Select [CatalogId] From @Catalog)

	Delete From [App_DataDictionary].[DatabaseColumn]
	Where	[CatalogId] In (Select [CatalogId] From @Catalog)

	Delete From [App_DataDictionary].[DatabaseTable]
	Where	[CatalogId] In (Select [CatalogId] From @Catalog)

	Delete From [App_DataDictionary].[DatabaseSchema]
	Where	[CatalogId] In (Select [CatalogId] From @Catalog)

	Delete From [App_DataDictionary].[ApplicationCatalog]
	Where	[ModelId] = @ModelId And
			[CatalogId] In (Select [CatalogId] From @Catalog)

	Delete From [App_DataDictionary].[DatabaseCatalog]
	Where	[CatalogId] In (Select [CatalogId] From @Catalog)

	Delete From [App_DataDictionary].[DomainAttributeProperty]
	Where	[AttributeId] In (Select [AttributeId] From @Attribute)

	Delete From [App_DataDictionary].[DomainAttributeAlias]
	Where	[AttributeId] In (Select [AttributeId] From @Attribute)

	Delete From [App_DataDictionary].[ApplicationAttribute]
	Where	[ModelId] = @ModelId And
			[AttributeId] In (Select [AttributeId] From @Attribute)

	Delete From [App_DataDictionary].[DomainAttribute]
	Where	[AttributeId] In (Select [AttributeId] From @Attribute)

	Delete From [App_DataDictionary].[DomainEntityProperty]
	Where	[EntityId] In (Select [EntityId] From @Entity)

	Delete From [App_DataDictionary].[DomainEntityAlias]
	Where	[EntityId] In (Select [EntityId] From @Entity)

	Delete From [App_DataDictionary].[ApplicationEntity]
	Where	[ModelId] = @ModelId And
			[EntityId] In (Select [EntityId] From @Entity)

	Delete From [App_DataDictionary].[DomainEntity]
	Where	[EntityId] In (Select [EntityId] From @Entity)

	Delete From [App_DataDictionary].[ApplicationModel]
	Where	[ModelId] = @ModelId

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