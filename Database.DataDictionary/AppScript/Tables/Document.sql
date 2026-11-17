CREATE TABLE [AppScript].[Document]
(
	[DocumentId]			UniqueIdentifier Not Null CONSTRAINT [DF_DocumentId] DEFAULT (newid()),
	[TemplateId]			UniqueIdentifier Not Null,
	[DocumentTitle]			[AppGeneral].[uddtTitle] Not Null, -- Title, independet of the file names.
	-- Input
	[SchemaId]				UniqueIdentifier Null, -- Null = External XML (Sub-Type)
	[StructuredFileName]	[AppGeneral].[uddtFileName] Null, -- Structured Data File Name (XML). Normally, Not Null.
	-- Process
	[TransformId]			UniqueIdentifier Null, -- Null = External Transform XSLT (Sub-Type)
	[TransformFileName]		[AppGeneral].[uddtFileName] Null, -- Normally, Not Null.
	-- Output
	[ScriptedFileName]		[AppGeneral].[uddtFileName] Null, -- Normally, Not Null.
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Document_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Document_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
	CONSTRAINT [AK_Document] UNIQUE ([TemplateId] ASC, [DocumentId] ASC), -- Used by FK's
	CONSTRAINT [FK_Document] FOREIGN KEY ([TemplateId]) REFERENCES [AppScript].[Template] ([TemplateId]),
	CONSTRAINT [FK_DocumentTransform] FOREIGN KEY ([TransformId]) REFERENCES [AppScript].[Transform] ([TransformId]),
	CONSTRAINT [FK_DocumentSchema] FOREIGN KEY ([SchemaId]) REFERENCES [AppScript].[SchemaDefinition] ([SchemaId]),
)	WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsScript].[Document]))
