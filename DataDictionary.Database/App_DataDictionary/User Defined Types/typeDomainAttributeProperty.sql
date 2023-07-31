CREATE TYPE [App_DataDictionary].[typeDomainAttributeProperty] AS TABLE (
	-- TIP: This matches the C# DataTable structure
    [AttributeId]          UNIQUEIDENTIFIER NULL,
	[PropertyId]           INT NULL,
	[PropertyValue]        NVarChar(4000) NULL,
    [SysStart]             DATETIME2 (7) NULL);
