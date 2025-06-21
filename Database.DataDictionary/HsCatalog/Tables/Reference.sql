CREATE TABLE [HsCatalog].[Reference]
(
	[ReferenceId]             UniqueIdentifier Not Null,
	[CatalogId]               UniqueIdentifier Not Null,
	[SchemaName]              SysName Not Null,
	[ObjectName]              SysName Not Null,
	[ObjectType]		      [AppGeneral].[dtObjectType] Not Null,
	[ReferencedDatabaseName]  SysName Null,
	[ReferencedSchemaName]    SysName Null,
	[ReferencedObjectName]    SysName Null,
	[ReferencedColumnName]    SysName Null,
	[ReferencedType]          [AppGeneral].[dtObjectType] Null,
	[IsCallerDependent]       Bit Null,
	[IsAmbiguous]             Bit Null,
	[IsSelected]              Bit Null,
	[IsModified]              Bit Null,
	[IsSelectAll]             Bit Null,
	[IsAllColumnsFound]       Bit Null,
	[IsInsertAll]             Bit Null,
	[IsIncomplete]            Bit NULL,
	[SysStart]                DATETIME2 (7) NOT NULL,
	[SysEnd]                  DATETIME2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_Reference]
    ON [HsCatalog].[Reference]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Reference]
    ON [HsCatalog].[Reference]([ReferenceId])
GO