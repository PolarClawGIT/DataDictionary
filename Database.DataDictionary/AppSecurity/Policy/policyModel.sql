CREATE SECURITY POLICY [AppSecurity].[policyModel]
	ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization] ([ModelId], 1) ON [AppModel].[Model],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization] ([ModelId], 1) ON [AppModel].[SubjectArea],

	ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization] ([ModelId], 1) ON [AppModel].[ModelAttribute],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization] ([ModelId], 1) ON [AppCatalog].[CatalogModel],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization] ([ModelId], 1) ON [AppModel].[ModelDefinition],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization] ([ModelId], 1) ON [AppModel].[ModelEntity],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization] ([ModelId], 1) ON [AppModel].[ModelProcess],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization] ([ModelId], 1) ON [AppModel].[ModelProperty],

	ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization] (Null, Null) ON [AppModel].[DefinitionEnumeration],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization] (Null, Null) ON [AppModel].[PropertyEnumeration],
	ADD BLOCK PREDICATE [AppSecurity].[funcModelAuthorization] (Null, Null) ON [AppModel].[AliasNameSpace]

	WITH (STATE = ON, SCHEMABINDING = ON)
GO