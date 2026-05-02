CREATE TABLE [AppScript].[SchemaDefinition]
(	-- Defines an XSD
	-- Note: The Suffix 'Definition' is redundent to avoid Name collesions in the application.
	-- It is not needed at the Database Layer.
	[SchemaId]				UniqueIdentifier Not Null CONSTRAINT [DF_SchemaId] DEFAULT (newid()),
	[TemplateId]            UniqueIdentifier Not Null,
	[SchemaTitle]			[AppGeneral].[uddtTitle] Not Null,
	-- Root Node Behavior
	[ForEachScope]			[AppGeneral].[uddtScopeName] Not Null,  -- Object Selection. ForEach Object of this type in the Model. Top Level, Model = everything in one file.
	[RootNodeName]			[AppGeneral].[uddtMember] Null, -- Name of the Root Node. Name of the Object is used if Null.
	-- Folder Patern for the files (Output for XSD, Input for XSLT)
	[RootFolder]			[AppGeneral].[uddtFileRoot] Null, -- Name of the Special Folder used as the Root defined in the Application.
	[RelativePath]			[AppGeneral].[uddtFilePath] Null,
	[FilePrefix]			[AppGeneral].[uddtFileAffix] Null,
	[FileSuffix]			[AppGeneral].[uddtFileAffix] Null,
	[FileExtension]			[AppGeneral].[uddtFileExtension] Null, -- XML is expected
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Schema_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Schema_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Schema] PRIMARY KEY CLUSTERED ([SchemaId] ASC),
	CONSTRAINT [AK_Schema] UNIQUE ([TemplateId] ASC, [SchemaId] ASC), -- Used by FK's
	CONSTRAINT [AK_SchemaTitle] UNIQUE ([TemplateId], [SchemaTitle] ASC),
	CONSTRAINT [FK_SchemaTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [AppScript].[Template] ([TemplateId]),
)	WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsScript].[SchemaDefinition]))
