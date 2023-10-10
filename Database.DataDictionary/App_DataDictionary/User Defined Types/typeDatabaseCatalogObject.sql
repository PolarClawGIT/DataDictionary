CREATE TYPE [App_DataDictionary].[typeDatabaseCatalogObject] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[SchemaName]             SysName Null,
	[ObjectName]             SysName Null -- Generic Object Name. Maybe Table, Routine, Domain or Constraint
)
