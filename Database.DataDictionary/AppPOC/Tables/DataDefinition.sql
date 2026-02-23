CREATE TABLE [AppPOC].[DataDefinition]
(	-- Describes the Data (XSD)
	[DataId]				UniqueIdentifier Not Null CONSTRAINT [DF_DataDefinitionId] DEFAULT (newid()),
	[DataTitle]				[AppGeneral].[uddtTitle] Not Null,
	[DataDescription]		[AppGeneral].[uddtDescription] Null,

	[RootNodeName]			[AppGeneral].[uddtMember] Null, -- Name of the Root Node. Name of the Object is used if Null.
	[BreakOnScope]			[AppGeneral].[uddtScopeName] NULL,  -- The Scope to have a document break on. Null = no break (single file).
	-- Folder Patern for the files
	[RootFolder]			[AppGeneral].[uddtFileRoot] Null, -- Name of the Special Folder used as the Root defined in the Application.
	[RelativePath]			[AppGeneral].[uddtFilePath] Null,
	[FilePrefix]			[AppGeneral].[uddtFileAffix] Null,
	[FileSuffix]			[AppGeneral].[uddtFileAffix] Null,
	[FileExtension]			[AppGeneral].[uddtFileExtension] Null, -- XML is expected
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DataDefinition_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DataDefinition_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DataDefinition] PRIMARY KEY CLUSTERED ([DataId] ASC),
	CONSTRAINT [AK_DataDefinitionTitle] UNIQUE ([DataTitle] ASC),
)
