CREATE TABLE [AppScript].[SchemaDocument]
(	-- Document that is an ouput of the Schema (XML data)
	-- This is a Sub-Type of Document using Roll-Down
	[DocumentId]		UniqueIdentifier Not Null,
	[TemplateId]		UniqueIdentifier Not Null,
	[SchemaId]			UniqueIdentifier Not Null, 
	[ObjectId]			UniqueIdentifier Null, -- Null = Fixed content
	[FileName]			[AppGeneral].[uddtFileName] Not Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_SchemaDocument_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_SchemaDocument_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_SchemaDocument] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
	CONSTRAINT [AK_SchemaDocument] UNIQUE ([TemplateId] ASC, [DocumentId] ASC), -- Used by FK's
	CONSTRAINT [FK_SchemaDocumentSchema] FOREIGN KEY ([TemplateId], [SchemaId]) REFERENCES [AppScript].[SchemaDefinition] ([TemplateId], [SchemaId]),
	CONSTRAINT [FK_SchemaDocumentObject] FOREIGN KEY ([TemplateId], [ObjectId]) REFERENCES [AppScript].[TemplateObject] ([TemplateId], [ObjectId]),
)	WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsScript].[SchemaDocument]))
