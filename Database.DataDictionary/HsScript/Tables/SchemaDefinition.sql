CREATE TABLE [HsScript].[SchemaDefinition]
(	-- Defines an XSD
	[SchemaId]				UniqueIdentifier Not Null,
	[TemplateId]            UniqueIdentifier Not Null,
	[SchemaTitle]			[AppGeneral].[uddtTitle] Not Null,
	-- Root Node Behavior
	[RootNodeName]			[AppGeneral].[uddtMember] Null, -- Name of the Root Node. Name of the Object is used if Null.
	[BreakOnScope]			[AppGeneral].[uddtScopeName] Null,  -- The Scope to have a document break on. Null = no break (single file).
	-- Folder Patern for the files (Output for XSD, Input for XSLT)
	[RootFolder]			[AppGeneral].[uddtFileRoot] Null, -- Name of the Special Folder used as the Root defined in the Application.
	[RelativePath]			[AppGeneral].[uddtFilePath] Null,
	[FilePrefix]			[AppGeneral].[uddtFileAffix] Null,
	[FileSuffix]			[AppGeneral].[uddtFileAffix] Null,
	[FileExtension]			[AppGeneral].[uddtFileExtension] Null, -- XML is expected
	-- Temporal History Support
	[SysStart]				DateTime2 (7) Not Null,
	[SysEnd]				DateTime2 (7)  Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_SchemaDefinition]
    ON [HsScript].[SchemaDefinition]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_SchemaDefinition]
    ON [HsScript].[SchemaDefinition]([SchemaId] ASC)
GO
