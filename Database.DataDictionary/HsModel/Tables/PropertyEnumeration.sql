CREATE TABLE [HsModel].[PropertyEnumeration]
(
	[PropertyId]             UniqueIdentifier Not Null,
	[PropertyTitle]          [AppGeneral].[dtTitle] Not Null, -- Title of the Property as it appears in the application. This may contain the Property Name but must be unique for each type of Extended Property it applies to.
	[PropertyDescription]    [AppGeneral].[dtDescription] Null,
	[IsCommon]               Bit Not Null,
	[DataType]               NVarChar(20) Not Null,
	[PropertyData]           NVarChar(2000) Null,
	[SysStart]               DateTime2 (7) NOT NULL,
	[SysEnd]                 DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_PropertyType]
    ON [HsModel].[PropertyEnumeration]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_PropertyType]
    ON [HsModel].[PropertyEnumeration]([PropertyId] ASC)
GO