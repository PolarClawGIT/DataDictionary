CREATE TABLE [HsModel].[Attribute]
(
	[AttributeId]			UniqueIdentifier Not Null,
	[AttributeTitle]		[AppGeneral].[uddtTitle] Not Null,
	[AttributeDescription]	[AppGeneral].[uddtDescription] Null,
	[AttributeName]			[AppGeneral].[uddtQualifiedName] Null,
	[DataType]			    NVarChar(128) Null,
	[DataLength]		    SmallInt Null,
	[DataPrecision]		    TinyInt Null,
	[DataScale]				TinyInt Null,
	[IsSingleValue]			Bit Null,
	[IsSimpleType]			Bit Null,
	[IsIntegral]			Bit Null,
	[IsNullable]			Bit Null,
	[IsKey]					Bit Null,
    [SysStart]				DateTime2 (7) NOT NULL,
    [SysEnd]				DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_Attribute]
    ON [HsModel].[Attribute]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Attribute]
    ON [HsModel].[Attribute]([AttributeId] ASC)
GO
