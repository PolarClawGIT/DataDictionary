CREATE TABLE [ProofOfConcept].[SchemaDefinition]
(	-- Describes the Data Scheme aka its Schema (XSD)
	[DefinitionId]				UniqueIdentifier Not Null CONSTRAINT [DF_SchemaDefinitionId] DEFAULT (newid()),
	[DefinitionTitle]			[AppGeneral].[uddtTitle] Not Null,
	[DefinitionDescription]		[AppGeneral].[uddtDescription] Null,

	[RootNodeName]			[AppGeneral].[uddtMember] Null, -- Name of the Root Node. Name of the Object is used if Null.
	[BreakOnScope]			[AppGeneral].[uddtScopeName] NULL,  -- The Scope to have a document break on. Null = no break (single file).
	-- Folder Patern for the files
	[RootFolder]			[AppGeneral].[uddtFileRoot] Null, -- Name of the Special Folder used as the Root defined in the Application.
	[RelativePath]			[AppGeneral].[uddtFilePath] Null,
	[FilePrefix]			[AppGeneral].[uddtFileAffix] Null,
	[FileSuffix]			[AppGeneral].[uddtFileAffix] Null,
	[FileExtension]			[AppGeneral].[uddtFileExtension] Null, -- XML is expected
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_SchemaDefinition_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_SchemaDefinition_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_SchemaDefinition] PRIMARY KEY CLUSTERED ([DefinitionId] ASC),
	CONSTRAINT [AK_SchemaDefinitionTitle] UNIQUE ([DefinitionTitle] ASC),
)
