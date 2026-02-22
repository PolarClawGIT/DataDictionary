CREATE TABLE [AppPOC].[TemplateTransform]
(
	[TransformId]			UniqueIdentifier Not Null CONSTRAINT [DF_TemplateTransformId] DEFAULT (newid()),
	[TemplateId]            UniqueIdentifier Not Null,
	--[DataId]				UniqueIdentifier Not Null,

	-- XSLT Transform Script. Not sure how to specify this is xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	[TransformScript]		XML Null, -- Can be imported/exported to file

	-- File Pattern to use (Output)
	[RootFolder]			[AppGeneral].[uddtFileRoot] Null, -- Name of the Special Folder used as the Root defined in the Application.
	[RelativePath]			[AppGeneral].[uddtFilePath] Null,
	[FilePrefix]			[AppGeneral].[uddtFileAffix] Null,
	[FileSuffix]			[AppGeneral].[uddtFileAffix] Null,
	[FileExtension]			[AppGeneral].[uddtFileExtension] Null, -- XML, TXT, SQL, CS, VB, MD ...
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateTransform_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateTransform_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_TemplateTransform] PRIMARY KEY CLUSTERED ([TransformId] ASC),
	CONSTRAINT [FK_TemplateTransformTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [AppPOC].[Template] ([TemplateId]),
)
