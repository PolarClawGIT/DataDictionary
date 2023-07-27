CREATE TYPE [App_DataDictionary].[typeDatabaseExtendedProperty] AS TABLE
(
	[PropertyId]             UniqueIdentifier Null,
	[CatalogId]              UniqueIdentifier Null,
	[CatalogName]            SysName          Null,
	-- Parameters for [fn_listextendedproperty]
	[Level0Type]             SysName          Null,
	[Level0Name]             SysName          Null,
	[Level1Type]             SysName          Null,
	[Level1Name]             SysName          Null,
	[Level2Type]             SysName          Null,
	[Level2Name]             SysName          Null,
	-- Results from [fn_listextendedproperty]
	[ObjType]                SysName          Null,
	[ObjName]                SysName          Null,
	[PropertyName]           SysName          Null,
	[PropertyValue]          NVarChar(Max)    Null
)
