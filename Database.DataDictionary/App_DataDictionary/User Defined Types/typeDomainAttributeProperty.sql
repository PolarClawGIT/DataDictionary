CREATE TYPE [App_DataDictionary].[typeDomainAttributeProperty] AS TABLE (
	-- TIP: This matches the C# DataTable structure
    [AttributeId]           UNIQUEIDENTIFIER NULL,
	[PropertyId]            UNIQUEIDENTIFIER NULL,
	[PropertyValue]         NVarChar(4000)  NULL,
	[DefinitionText]        NVarChar(Max) Null
	);
