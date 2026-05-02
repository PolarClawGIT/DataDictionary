CREATE TABLE [HsScript].[TransformDocument]
(	
	[DocumentId]		UniqueIdentifier Not Null,
	[TemplateId]		UniqueIdentifier Not Null,
	[TransformId]		UniqueIdentifier Not Null, 
	[SchemaDocumentId]	UniqueIdentifier Not Null,
	[FileName]			[AppGeneral].[uddtFileName] Not Null,
	-- Temporal History Support
	[SysStart]			DateTime2 (7) Not Null,
	[SysEnd]			DateTime2 (7) Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_TransformDocument]
    ON [HsScript].[TransformDocument]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_TransformDocument]
    ON [HsScript].[TransformDocument]([DocumentId] ASC)
GO