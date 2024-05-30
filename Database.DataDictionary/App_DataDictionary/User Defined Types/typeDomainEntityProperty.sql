CREATE TYPE [App_DataDictionary].[typeDomainEntityProperty] AS TABLE (
	-- TIP: This matches the C# DataTable structure
    [EntityId]              UNIQUEIDENTIFIER NULL,
	[PropertyId]            UNIQUEIDENTIFIER NULL,
	[PropertyValue]         NVarChar(4000)  NULL
);