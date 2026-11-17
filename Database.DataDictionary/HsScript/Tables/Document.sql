CREATE TABLE [HsScript].[Document]
(
	[DocumentId]			UniqueIdentifier Not Null,
	[TemplateId]			UniqueIdentifier Not Null,
	[DocumentTitle]			[AppGeneral].[uddtTitle] Not Null,
	[SchemaId]				UniqueIdentifier Null,
	[StructuredFileName]	[AppGeneral].[uddtFileName] Null,
	[TransformId]			UniqueIdentifier Null,
	[TransformFileName]		[AppGeneral].[uddtFileName] Null,
	[ScriptedFileName]		[AppGeneral].[uddtFileName] Null,
	-- Temporal History Support
	[SysStart]				DateTime2 (7) Not Null,
	[SysEnd]				DateTime2 (7) Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_Document]
    ON [HsScript].[Document]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Document]
    ON [HsScript].[Document]([DocumentId] ASC)
GO
