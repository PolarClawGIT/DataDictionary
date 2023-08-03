CREATE TYPE [App_DataDictionary].[typeApplicationProperty] AS TABLE
(    -- TIP: This matches the C# DataTable structure
	[PropertyId]         UniqueIdentifier Null,
	[PropertyTitle]      NVarChar(100) Null,
	[PropertyName]       SysName Null
)
