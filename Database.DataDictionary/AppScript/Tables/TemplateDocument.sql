CREATE TABLE [AppScript].[TemplateDocument]
(	-- POC Table. Replaces [SchemaDocument] & [TransformDocument]
	-- Represents a single object that is to be processed.
	[DocumentId]		UniqueIdentifier Not Null CONSTRAINT [DF_DocumentId] DEFAULT (newid()),
	[TemplateId]		UniqueIdentifier Not Null,
	[DocumentTitle]		[AppGeneral].[uddtTitle] Not Null, -- Title, independet of the file names.
	-- Input
	[SchemaId]			UniqueIdentifier Null, -- Null = External XML (Sub-Type)
	[ObjectId]			UniqueIdentifier Null, -- Null = External XML (Sub-Type), Required if [SchemaId] is specfied.
	[DataFileName]		[AppGeneral].[uddtFileName] Null, -- Null = defined by [SchemaId] & [ObjectId] rules, otherwise this is an override/external.
	-- Process
	[TransformId]		UniqueIdentifier Null, -- Null = External XSLT (Sub-Type)
	[TransformFileName]	[AppGeneral].[uddtFileName] Null, -- Null = defined by [TransformId] rules, otherwise this is an override/external.
	-- Output
	[ScriptedFileName]	[AppGeneral].[uddtFileName] Null, -- Null = defined by [TransformId] & [ObjectId] rules, otherwise this is an override.
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateDocument_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateDocument_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_TemplateDocument] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
	CONSTRAINT [AK_TemplateDocument] UNIQUE ([TemplateId] ASC, [DocumentId] ASC), -- Used by FK's
	CONSTRAINT [FK_TemplateDocument] FOREIGN KEY ([TemplateId]) REFERENCES [AppScript].[Template] ([TemplateId]),
	CONSTRAINT [FK_TemplateDocumentTransform] FOREIGN KEY ([TemplateId], [TransformId]) REFERENCES [AppScript].[Transform] ([TemplateId], [TransformId]),
	CONSTRAINT [FK_TemplateDocumentSchema] FOREIGN KEY ([TemplateId], [SchemaId]) REFERENCES [AppScript].[SchemaDefinition] ([TemplateId], [SchemaId]),
	CONSTRAINT [FK_TemplateDocumentObject] FOREIGN KEY ([TemplateId], [ObjectId]) REFERENCES [AppScript].[TemplateObject] ([TemplateId], [ObjectId]),
	CONSTRAINT [CK_TemplateDocumentObjectId] CHECK (([SchemaId] is Null And [ObjectId] is Null) Or ([SchemaId] is Not Null And [ObjectId] is Not Null))
)	WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsScript].[TemplateDocument]))
