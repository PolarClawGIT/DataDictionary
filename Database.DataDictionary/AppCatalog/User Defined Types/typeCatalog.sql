CREATE TYPE [AppCatalog].[typeCatalog] AS TABLE (
	-- TIP: This matches the C# DataTable structure and results of the Get method.
	[CatalogId]            UniqueIdentifier Null,
	[CatalogTitle]         [App_DataDictionary].[typeTitle] Null,
	[CatalogDescription]   [App_DataDictionary].[typeDescription] Null,
	[SourceServerName]     SysName          Null,
	[SourceDatabaseName]   SysName          Null,
	[SourceDate]           DateTime         Null,
	[CreatedBy]            SysName Null,
	[CreatedOn]            DateTime2 (7) Null,
	[IsInserted]           Bit Null,
	[IsUpdated]            Bit Null,
	[IsDeleted]            Bit Null,
	[IsCurrent]            Bit Null
);
