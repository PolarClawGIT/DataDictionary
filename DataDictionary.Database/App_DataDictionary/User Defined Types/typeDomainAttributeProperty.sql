CREATE TYPE [App_DataDictionary].[typeDomainAttributeProperty] AS TABLE (
	-- TIP: This matches the C# DataTable structure
    [AttributeId]           UNIQUEIDENTIFIER NULL,
	[PropertyId]            UNIQUEIDENTIFIER NULL,
	[AttributePropertyDescription] NVARCHAR (1000)  NULL,
	[DefinitionText]        NVarChar(Max) Null,
	[ExtendedPropertyValue] SysName Null,
	[ChoiceValue]           NVarChar(50) Null,
    [SysStart]              DATETIME2 (7) NULL);
