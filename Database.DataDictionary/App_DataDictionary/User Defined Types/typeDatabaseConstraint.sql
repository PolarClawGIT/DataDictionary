CREATE TYPE [App_DataDictionary].[typeDatabaseConstraint] AS TABLE (
    [CatalogId]            UNIQUEIDENTIFIER                     NULL,
    [ConstraintId]         UNIQUEIDENTIFIER                     NULL,
    [DatabaseName]         [sysname]                            NULL,
    [SchemaName]           [sysname]                            NULL,
    [ConstraintName]       [sysname]                            NULL,
    [TableName]            [sysname]                            NULL,
    [ConstraintType]       NVARCHAR (60)                        NULL);