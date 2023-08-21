CREATE TYPE [App_DataDictionary].[typeDatabaseCatalogObject] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	--[CatalogName]            SysName Null,
	[SchemaName]             SysName Null,
	[ObjectName]             SysName Null -- Generic Object Name. Maybe Table, Routine, Domain or Constraint
)
