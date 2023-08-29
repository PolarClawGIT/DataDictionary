CREATE TYPE [App_DataDictionary].[typeApplicationDefinition] AS TABLE
(    -- TIP: This matches the C# DataTable structure
	[DefinitionId]           UniqueIdentifier NULL,
	[DefinitionTitle]        [App_DataDictionary].[typeTitle] Null,
	[DefinitionDescription]  [App_DataDictionary].[typeDescription] Null,
	[Obsolete]               Bit Null,
	[SysStart]               DATETIME2 Null
)
