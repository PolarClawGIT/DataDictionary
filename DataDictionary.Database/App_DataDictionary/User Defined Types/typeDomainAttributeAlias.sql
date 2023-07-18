CREATE TYPE [App_DataDictionary].[typeDomainAttributeAlias] AS TABLE (
	-- TIP: This matches the C# DataTable structure
    [AttributeId]          UNIQUEIDENTIFIER NULL,
	[CatalogName]          SYSNAME          NULL,
	[SchemaName]           SYSNAME          NULL,
	[ObjectName]           SYSNAME          NULL,
	[ElementName]          SYSNAME          NULL,
    [SysStart]             DATETIME2 (7)    NULL);