CREATE TYPE [AppCatalog].[typeProperty] AS TABLE
(
	[CatalogId]              UniqueIdentifier Null,
	[PropertyId]             UniqueIdentifier Null,
	[DatabaseName]           SysName          Null,
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
	[PropertyValue]          NVarChar(Max)    Null,
	[CreatedBy]              SysName Null,
	[CreatedOn]              DateTime2 (7) Null,
	[IsInserted]             Bit Null,
	[IsUpdated]              Bit Null,
	[IsDeleted]              Bit Null,
	[IsCurrent]              Bit Null
)
