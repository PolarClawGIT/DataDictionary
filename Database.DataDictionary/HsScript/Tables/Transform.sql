CREATE TABLE [HsScript].[Transform]
(	[TransformId]			UniqueIdentifier Not Null,
	[TransformTitle]		[AppGeneral].[uddtTitle] Not Null,
	[TemplateId]            UniqueIdentifier Not Null,
	[SchemaId]				UniqueIdentifier Null,
	[TransformScript]		XML Null,
	[TransformFileName]		[AppGeneral].[uddtFileName] Null,
	[RootFolder]			[AppGeneral].[uddtFileRoot] Null,
	[RelativePath]			[AppGeneral].[uddtFilePath] Null,
	[FilePrefix]			[AppGeneral].[uddtFileAffix] Null,
	[FileSuffix]			[AppGeneral].[uddtFileAffix] Null,
	[FileExtension]			[AppGeneral].[uddtFileExtension] Null,
	-- Temporal History Support
	[SysStart]			DateTime2 (7) Not Null,
	[SysEnd]			DateTime2 (7) Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_Transform]
    ON [HsScript].[Transform]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Transform]
    ON [HsScript].[Transform]([TransformId] ASC)
GO