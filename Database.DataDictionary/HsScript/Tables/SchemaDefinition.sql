CREATE TABLE [HsScript].[SchemaDefinition]
(	-- Defines an XSD
	[SchemaId]				UniqueIdentifier Not Null,
	[TemplateId]            UniqueIdentifier Not Null,
	[SchemaTitle]			[AppGeneral].[uddtTitle] Not Null,
	-- Root Node Behavior
	[ForEachScope]			[AppGeneral].[uddtScopeName] Not Null,
	[RootNodeName]			[AppGeneral].[uddtMember] Null,
	-- Folder Patern for the files (Output for XSD, Input for XSLT)
	[RootFolder]			[AppGeneral].[uddtFileRoot] Null,
	[RelativePath]			[AppGeneral].[uddtFilePath] Null,
	[FilePrefix]			[AppGeneral].[uddtFileAffix] Null,
	[FileSuffix]			[AppGeneral].[uddtFileAffix] Null,
	[FileExtension]			[AppGeneral].[uddtFileExtension] Null,
	-- Temporal History Support
	[SysStart]				DateTime2 (7) Not Null,
	[SysEnd]				DateTime2 (7) Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_SchemaDefinition]
    ON [HsScript].[SchemaDefinition]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_SchemaDefinition]
    ON [HsScript].[SchemaDefinition]([SchemaId] ASC)
GO
