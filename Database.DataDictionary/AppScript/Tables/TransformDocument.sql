CREATE TABLE [AppScript].[TransformDocument]
(	-- Document that is the results/output of the Transform
	-- This is a Sub-Type of Document using Roll-Down
	[DocumentId]		UniqueIdentifier Not Null,
	[TemplateId]		UniqueIdentifier Not Null,
	[TransformId]		UniqueIdentifier Not Null, 
	[SchemaDocumentId]	UniqueIdentifier Not Null, -- Source Document
	[FileName]			[AppGeneral].[uddtFileName] Not Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TransformDocument_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TransformDocument_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_TransformDocument] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
	CONSTRAINT [AK_TransformDocument] UNIQUE ([TemplateId] ASC, [DocumentId] ASC), -- Used by FK's
	CONSTRAINT [FK_TransformDocumentTransform] FOREIGN KEY ([TemplateId], [TransformId]) REFERENCES [AppScript].[Transform] ([TemplateId], [TransformId]),
	CONSTRAINT [FK_TransformDocumentSchema] FOREIGN KEY ([TemplateId], [SchemaDocumentId]) REFERENCES [AppScript].[SchemaDocument] ([TemplateId], [DocumentId]),
)	WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsScript].[TransformDocument]))
