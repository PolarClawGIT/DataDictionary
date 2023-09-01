CREATE TYPE [App_DataDictionary].[typeDomainAttributeDefinition] AS TABLE
(-- TIP: This matches the C# DataTable structure & Get procedure.
	[AttributeId]          UniqueIdentifier NULL,
	[DefinitionId]         UniqueIdentifier NULL,
	[DefinitionText]       NVarChar(Max)    NULL,
    [SysStart]             DATETIME2 (7)    NULL);
