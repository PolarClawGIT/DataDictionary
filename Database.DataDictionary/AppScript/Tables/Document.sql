CREATE TABLE [AppScript].[Document]
(	-- A document is a group of files to be attached to the Model.
	-- Input (XML output from the Script builder)
	-- Process (XSLT process defined in the Template
	-- Output (XML or Text that result from the XSLT)
	-- Files are kept on local system, not in the database.
	[DocumentId]            UniqueIdentifier NOT Null CONSTRAINT [DF_DocumentId] DEFAULT (newid()),
	[DocumentTitle]			[AppGeneral].[uddtTitle] Not Null,
	[ModelId]		        UniqueIdentifier NOT NULL,
	[TemplateId]            UniqueIdentifier NULL, -- Template used to generate this document
	[RootFolder]			[AppGeneral].[uddtFileRoot] Null, -- Name of the Special Folder used as the Root defined in the Application.
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Document_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Document_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
	CONSTRAINT [FK_ModelDocument] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
)
