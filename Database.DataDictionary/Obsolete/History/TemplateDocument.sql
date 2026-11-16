CREATE TABLE [HsScript].[TemplateDocument]
(	
	[DocumentId]		UniqueIdentifier Not Null,
	[TemplateId]		UniqueIdentifier Not Null,
	[DocumentTitle]		[AppGeneral].[uddtTitle] Not Null,
	[SchemaId]			UniqueIdentifier Null,
	[ObjectId]			UniqueIdentifier Null,
	[DataFileName]		[AppGeneral].[uddtFileName] Null,
	[TransformId]		UniqueIdentifier Null,
	[TransformFileName]	[AppGeneral].[uddtFileName] Null,
	[ScriptedFileName]	[AppGeneral].[uddtFileName] Null,
	-- Temporal History Support
	[SysStart]			DateTime2 (7) Not Null,
	[SysEnd]			DateTime2 (7) Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_TemplateDocument]
    ON [HsScript].[TemplateDocument]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_TemplateDocument]
    ON [HsScript].[TemplateDocument]([DocumentId] ASC)
GO