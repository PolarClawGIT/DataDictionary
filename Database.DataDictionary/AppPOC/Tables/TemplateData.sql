CREATE TABLE [AppPOC].[TemplateData]
(	-- Repesents the XML data
	[TemplateId]            UniqueIdentifier Not Null,
	--[ModelId]				UniqueIdentifier Not Null, -- Data is Model specific Each Model can have muliple Data for the same template.
	[DataId]				UniqueIdentifier Not Null CONSTRAINT [DF_TemplateDataId] DEFAULT (newid()),

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
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateData_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateData_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_TemplateData] PRIMARY KEY CLUSTERED ([DataId] ASC),
	--CONSTRAINT [AK_TemplateDataModel] UNIQUE ([ModelId], [DataTitle]),
	CONSTRAINT [AK_TemplateDataTitle] UNIQUE ([TemplateId], [DataTitle]),
	CONSTRAINT [FK_TemplateDataTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [AppPOC].[Template] ([TemplateId]),
	--CONSTRAINT [FK_TemplateDataModel] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
)
