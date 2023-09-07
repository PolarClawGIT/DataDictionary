CREATE TYPE [App_DataDictionary].[typeApplicationProperty] AS TABLE
(    -- TIP: This matches the C# DataTable structure
	[PropertyId]          UniqueIdentifier Null,
	[PropertyTitle]       [App_DataDictionary].[typeTitle] Null,
	[PropertyDescription] [App_DataDictionary].[typeDescription] Null,
	[IsExtendedProperty]  Bit Null,
	[IsDefinition]        Bit Null,
	[IsChoice]            Bit Null,
	[ExtendedProperty]    SysName Null,
	[ChoiceList]          NVarChar(2000) Null,
	[Obsolete]            Bit Null,
	[SysStart]            DATETIME2 Null
)
