CREATE TYPE [App_DataDictionary].[typeScriptingSchemaElement] AS TABLE
(
	[ElementId]             UniqueIdentifier NULL,
	[SchemaId]              UniqueIdentifier NULL,
	[ScopeName]             [App_DataDictionary].[typeScopeName] Null,
	[ColumnName]            SysName  Null,
	[DataName]              SysName Null, 
	[DataType]              SysName Null, 
	[DataNillable]          Bit Null,
	[AsElement]             Bit Null, 
	[AsAttribute]           Bit Null,
	[DataAsText]            Bit Null,
	[DataAsCData]           Bit Null,
	[DataAsXml]             Bit Null
)
