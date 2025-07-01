CREATE TABLE [HsModel].[DefinitionEnumeration]
(
	[DefinitionId]             UniqueIdentifier NOT NULL,
	[DefinitionTitle]          [AppGeneral].[uddtTitle] Not Null,
	[DefinitionDescription]    [AppGeneral].[uddtDescription] Null,
	[IsCommon]                 Bit Not Null DEFAULT(0),
	[SysStart]                 DateTime2 (7) NOT NULL,
	[SysEnd]                   DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_DefinitionType]
    ON [HsModel].[DefinitionEnumeration]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_DefinitionType]
    ON [HsModel].[DefinitionEnumeration]([DefinitionId] ASC)
GO