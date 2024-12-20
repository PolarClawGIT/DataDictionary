CREATE TABLE [HsModel].[Attribute]
(
	[AttributeId]          UniqueIdentifier Not Null,
	[AttributeTitle]       [App_DataDictionary].[typeTitle] Not Null,
	[AttributeDescription] [App_DataDictionary].[typeDescription] Null,
	[AttributeName]        [AppModel].[typeQualifiedName] Null,
	[IsSingleValue]        Bit Null,
	[IsSimpleType]         Bit Null,
	[IsIntegral]           Bit Null,
	[IsNullable]           Bit Null,
	[IsKey]                Bit Null,
    [SysStart]             DateTime2 (7) NOT NULL,
    [SysEnd]               DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_Attribute]
    ON [HsModel].[Attribute]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Attribute]
    ON [HsModel].[Attribute]([AttributeId] ASC)
GO
