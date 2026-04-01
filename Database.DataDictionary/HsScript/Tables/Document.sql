CREATE TABLE [HsScript].[Document]
(	[DocumentId]		UniqueIdentifier Not Null,
	[TemplateId]		UniqueIdentifier Not Null,
	[SchemaId]			UniqueIdentifier Null,
	[TransformId]		UniqueIdentifier Null,
	[ObjectId]			UniqueIdentifier Null,
	[FileName]			[AppGeneral].[uddtFileName] Not Null, -- Can be built from Data Object
	-- Temporal History Support
	[SysStart]			DateTime2 (7) Not Null,
	[SysEnd]			DateTime2 (7) Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_Document]
    ON [HsScript].[Document]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Document]
    ON [HsScript].[Document]([DocumentId] ASC)
GO
