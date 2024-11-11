CREATE SECURITY POLICY [AppSecurity].[policyCatalogDomain]
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogDomainAuthorization]([DomainId], 0)
		ON [AppCatalog].[Domain] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogDomainAuthorization]([DomainId], 0)
		ON [AppCatalog].[Domain] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcCatalogDomainAuthorization]([DomainId], 0)
		ON [AppCatalog].[Domain] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
