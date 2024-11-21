CREATE TYPE [AppCatalog].[typeConstraint] AS TABLE (
    [CatalogId]            UNIQUEIDENTIFIER                     NULL,
    [ConstraintId]         UNIQUEIDENTIFIER                     NULL,
    [DatabaseName]         [sysname]                            NULL,
    [SchemaName]           [sysname]                            NULL,
    [TableName]            [sysname]                            NULL,
    [ConstraintName]       [sysname]                            NULL,
    [ConstraintType]       NVARCHAR (60)                        NULL,
	-- Temporal Data
	[CreatedOn]            DateTime2 (7) Null,
	[CreatedBy]            NVarChar(4000) Null,
	[RemovedOn]            DateTime2 (7) Null,
	[RemovedBy]            NVarChar(4000) Null,
	[IsInserted]           Bit Null,
	[IsUpdated]            Bit Null,
	[IsDeleted]            Bit Null,
	[IsCurrent]            Bit Null);