CREATE VIEW [AppSecurity].[Securable]
AS
With [Securable] As (
	Select	[HelpId] As [SecurableId],
			[HelpSubject] As [SecurableTitle]
	From	[AppGeneral].[HelpSubject]
	-- To Be supported
	Union
	Select	[CatalogId] As [SecurableId],
			[CatalogTitle] As [SecurableTitle]
	From	[AppCatalog].[Catalog]
	Union
	Select	[LibraryId] As [SecurableId],
			[LibraryTitle] As [SecurableTitle]
	From	[AppLibrary].[LibrarySource]
	Union
	Select	[ModelId] As [SecurableId],
			[ModelTitle] As [SecurableTitle]
	From	[AppModel].[Model]
	Union
	Select	[TemplateId] As [SecurableId],
			[TemplateTitle] As [SecurableTitle]
	From	[AppScript].[Template]
	/* Not Supported
	Union
	Select	[AttributeId] As [ObjectId],
			[AttributeTitle] As [ObjectTitle]
	From	[App_DataDictionary].[DomainAttribute]
	Union
	Select	[EntityId] As [ObjectId],
			[EntityTitle] As [ObjectTitle]
	From	[App_DataDictionary].[DomainEntity]
	Union
	Select	[ProcessId] As [ObjectId],
			[ProcessTitle] As [ObjectTitle]
	From	[App_DataDictionary].[DomainProcess]
	Union
	Select	[RelationshipId] As [ObjectId],
			[RelationshipTitle] As [ObjectTitle]
	From	[App_DataDictionary].[DomainRelationship]
	Union
	Select	[PropertyId] As [ObjectId],
			[PropertyTitle] As [ObjectTitle]
	From	[App_DataDictionary].[DomainProperty]
	Union
	Select	[DefinitionId] As [ObjectId],
			[DefinitionTitle] As [ObjectTitle]
	From	[App_DataDictionary].[DomainDefinition]
	*/)
Select	IsNull(O.[SecurableId], S.[SecurableId]) As [SecurableId],
		IsNull(O.[SecurableTitle], '(Orphaned)') As [SecurableTitle],
		Convert(Bit,IIF(O.[SecurableId] is null,1,0)) As [IsOrphaned]
From	[Securable] O
		Full Outer Join (
			Select	[SecurableId] As [SecurableId]
			From	[AppSecurity].[SecurableOwner]
			Union
			Select	[SecurableId] As [SecurableId]
			From	[AppSecurity].[SecurablePermission]) S
		On	O.[SecurableId] = S.[SecurableId]
GO