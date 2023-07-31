CREATE TYPE [App_DataDictionary].[typeApplicationProperty] AS TABLE
(    -- TIP: This matches the C# DataTable structure
	[PropertyId]         UniqueIdentifier Null,
	[PropertyTitle]      NVarChar(100) Null,
	[ModelId]            UniqueIdentifier Null,
	[IsExtendedProperty] Bit Null,
	[PropertyName]       SysName Null,
	[ScopeType]          SysName Null,
	[ObjectType]         SysName Null,
	[ElementType]        SysName Null
)
