CREATE TYPE [App_DataDictionary].[typeDomainEntityAlias] AS TABLE (
	-- TIP: This matches the C# DataTable structure
    [EntityId]             UNIQUEIDENTIFIER NULL,
	[EntityAliasId]        INT              NULL,
	[CatalogName]          SYSNAME          NULL,
	[SchemaName]           SYSNAME          NULL,
	[ObjectName]           SYSNAME          NULL,
    [SysStart]             DATETIME2 (7)    NULL);