CREATE TYPE [App_DataDictionary].[typeModel] AS TABLE
(    -- TIP: This matches the C# DataTable structure
	[ModelId]              UniqueIdentifier Null,
	[ModelTitle]           NVarChar(100) Null,
	[ModelDescription]     NVarChar(1000) Null,
	[Obsolete]             Bit Null,
	[SysStart]             DATETIME2
)
