CREATE TYPE [AppCatalog].[typeSchema] AS TABLE (
	-- TIP: This matches the C# DataTable structure and results of the Get Procedure.
	[CatalogId]     UniqueIdentifier Null,
	[SchemaId]      UniqueIdentifier Null,
	[DatabaseName]  SysName Null,
	[SchemaName]    SysName Null,
	[CreatedBy]     SysName Null,
	[CreatedOn]     DateTime2 (7) Null,
	[IsInserted]    Bit Null,
	[IsUpdated]     Bit Null,
	[IsDeleted]     Bit Null,
	[IsCurrent]     Bit Null
)
