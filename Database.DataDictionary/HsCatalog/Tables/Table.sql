CREATE TABLE [HsCatalog].[Table]
(
	[TableId]             UniqueIdentifier Not Null,
	[SchemaId]            UniqueIdentifier Not Null,
	[TableName]           SysName Not Null,
	[TableType]           [AppGeneral].[uddtObjectType] Null, -- BASE TABLE, VIEW, HISTORY TABLE, TEMPTORAL TABLE
	[SysStart]            DATETIME2 (7) NOT NULL,
	[SysEnd]              DATETIME2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_Table]
    ON [HsCatalog].[Table]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Table]
    ON [HsCatalog].[Table]([TableId])
GO