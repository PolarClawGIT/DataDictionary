CREATE TABLE [AppScript].[Document]
(	-- POC code
	-- A document is a pair of files to be attached to the Model.
	-- The an XSL Transform may be included that is used to build the output.
	-- The XSL Transorm can come from a Template.
	-- Files are kept on local system, not in the database.
	[DocumentId]            UniqueIdentifier NOT Null CONSTRAINT [DF_DocumentId] DEFAULT (newid()),
	[DocumentTitle]			[AppGeneral].[uddtTitle] Not Null,
	[ModelId]		        UniqueIdentifier NOT NULL,
	[TemplateId]            UniqueIdentifier NULL, -- Template used to generate this document
	[TransformScript]		XML Null , -- XSLT Transform Script. Not sure how to specify this is xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	[RootFolder]			NVarChar(100) Null, -- Name of the Special Folder used as the Root
	[InputDirectory]		NVarChar(250) Null, -- From Root, the directory for the XML file.
	[InputFile]             NVarChar(100) Null, 
	[OutputDirectory]		NVarChar(250) Null, -- From Root, the directory for the result file.
	[OutputFile]            NVarChar(100) Null,
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Document_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Document_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
	CONSTRAINT [FK_ModelDocument] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
)
