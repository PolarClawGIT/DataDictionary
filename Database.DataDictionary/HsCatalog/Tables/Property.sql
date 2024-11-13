CREATE TABLE [HsCatalog].[Property]
(
	[PropertyId]     UniqueIdentifier Not Null,
	[CatalogId]      UniqueIdentifier Not Null,
	-- Parameters for [fn_listextendedproperty]
	[Level0Type]     SysName Null,
	[Level0Name]     SysName Null,
	[Level1Type]     SysName Null,
	[Level1Name]     SysName Null,
	[Level2Type]     SysName Null,
	[Level2Name]     SysName Null,
	-- Results from [fn_listextendedproperty]
	[ObjType]        SysName Not Null,
	[ObjName]        SysName Not Null,
	[PropertyName]   SysName Not Null,
	[PropertyValue]  NVarChar(Max) Null,
	[CreatedBy]      SysName Not Null,
	[SysStart]       DATETIME2 (7) NOT NULL,
	[SysEnd]         DATETIME2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_Property]
    ON [HsCatalog].[Property]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Property]
    ON [HsCatalog].[Property]([PropertyId])
GO