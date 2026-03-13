CREATE TABLE [AppScript].[Transform]
(	-- Defines an XSLT
	[TransformId]			UniqueIdentifier Not Null CONSTRAINT [DF_TransformId] DEFAULT (newid()),
	[TransformTitle]		[AppGeneral].[uddtTitle] Not Null,
	[TemplateId]            UniqueIdentifier Not Null,
	[SchemaId]				UniqueIdentifier Null, -- Input, Null = No Schema, everything must be hand mapped.
	-- XSLT Transform Script. Not sure how to specify this is xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	[TransformScript]		XML Null, -- Can be imported/exported to file. Null = Look for the File
	[TransformFileName]		[AppGeneral].[uddtFileName] Null, -- Null = Looks for Script
	-- File Pattern to use (Output)
	[RootFolder]			[AppGeneral].[uddtFileRoot] Null, -- Name of the Special Folder used as the Root defined in the Application.
	[RelativePath]			[AppGeneral].[uddtFilePath] Null,
	[FilePrefix]			[AppGeneral].[uddtFileAffix] Null,
	[FileSuffix]			[AppGeneral].[uddtFileAffix] Null,
	[FileExtension]			[AppGeneral].[uddtFileExtension] Null, -- XML, TXT, SQL, CS, VB, MD ...
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Transform_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Transform_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Transform] PRIMARY KEY CLUSTERED ([TransformId] ASC),
	CONSTRAINT [AK_Transform] UNIQUE ([TemplateId] ASC, [TransformId] ASC), -- Used by FK's
	CONSTRAINT [FK_TransformTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [AppScript].[Template] ([TemplateId]),
	CONSTRAINT [FK_TransformSchema] FOREIGN KEY ([TemplateId], [SchemaId]) REFERENCES [AppScript].[SchemaDefinition] ([TemplateId], [SchemaId]),
)	WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsScript].[Transform]))
