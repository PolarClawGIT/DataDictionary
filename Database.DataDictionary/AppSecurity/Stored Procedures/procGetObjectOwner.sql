CREATE PROCEDURE [AppSecurity].[procGetObjectOwner]
		@Object UniqueIdentifier = Null,
		@PrincipleId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Object Owner (Principle).
*/
;With [Titles] As (
	Select	[HelpId] As [ObjectId],
			[HelpSubject] As [ObjectTitle]
	From	[AppGeneral].[HelpSubject]
	/* Not yet supported
	Union
	Select	[CatalogId] As [ObjectId],
			[CatalogTitle] As [ObjectTitle]
	From	[App_DataDictionary].[DatabaseCatalog]
	Union
	Select	[LibraryId] As [ObjectId],
			[LibraryTitle] As [ObjectTitle]
	From	[App_DataDictionary].[LibrarySource]
	Union
	Select	[ModelId] As [ObjectId],
			[ModelTitle] As [ObjectTitle]
	From	[App_DataDictionary].[Model]
	Union
	Select	[TemplateId] As [ObjectId],
			[TemplateTitle] As [ObjectTitle]
	From	[App_DataDictionary].[ScriptingTemplate]
	*/
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
Select	O.[PrincipleId],
		O.[ObjectId],
		T.[ObjectTitle],
		Convert(Bit, IIF(
				S.[IsSecurityAdmin] = 1 Or
				S.[IsOwner] = 1 ,1,0))
				As [AlterValue],
		Convert(Bit, IIF(
				S.[IsSecurityAdmin] = 1 Or
				S.[IsOwner] = 1 ,1,0))
				As [AlterSecurity]
From	[AppSecurity].[ObjectOwner] O
		Inner Join [Titles] T
		On	O.[ObjectId] = T.[ObjectId]
		Cross Apply [AppSecurity].[funcAuthorization](O.[ObjectId]) S
Where	(@Object is Null Or O.[ObjectId] = @Object) And
		(@PrincipleId is Null Or O.[PrincipleId] = @PrincipleId)
GO