CREATE TABLE [ProofOfConcept].[TemplateDocument]
(	-- Supertype: XSLT Input & Output
	[TemplateId]            UniqueIdentifier Not Null,
	[DocumentId]            UniqueIdentifier Not Null,
	-- all Null: (Input, fixed)
	-- [DefinitionId] & [ObjectId]: Transform source document (Input, XML)
	-- [TransformId] & [ObjectId]: Transform result (Output)
	-- [TransformId]: Transform result (Output, fixed)
	[DefinitionId]			UniqueIdentifier Null,
	[TransformId]			UniqueIdentifier Null,
	[ObjectId]				UniqueIdentifier Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateDocument_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateDocument_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_TemplateDocument] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [DocumentId] ASC),
	CONSTRAINT [FK_TemplateDocumentTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [ProofOfConcept].[Template] ([TemplateId]),
	CONSTRAINT [FK_TemplateDocumentDocument] FOREIGN KEY ([DocumentId]) REFERENCES [ProofOfConcept].[Document] ([DocumentId]),
	CONSTRAINT [FK_TemplateDocumentObject] FOREIGN KEY ([TemplateId], [ObjectId]) REFERENCES [ProofOfConcept].[TemplateObject] ([TemplateId], [ObjectId]),
	CONSTRAINT [FK_TemplateDocumentTransform] FOREIGN KEY ([TemplateId], [TransformId]) REFERENCES [ProofOfConcept].[TemplateTransform] ([TemplateId], [TransformId]),
	CONSTRAINT [FK_TemplateDocumentDefinition] FOREIGN KEY ([TemplateId], [DefinitionId]) REFERENCES [ProofOfConcept].[TemplateTransform] ([TemplateId], [DefinitionId]),
	CONSTRAINT [CK_TemplateDocumentType] CHECK (
	([DefinitionId] is Null and [TransformId] is Null And [ObjectId] is Null) Or -- Input, Fixed
	([DefinitionId] is Not Null and [TransformId] is Null And [ObjectId] is Not Null) Or -- Input, object
	([DefinitionId] is Null and [TransformId] is Not Null And [ObjectId] is Not Null) Or -- Output, object
	([DefinitionId] is Null and [TransformId] is Not Null And [ObjectId] is Null)), -- Output, Fixed
)
GO
CREATE INDEX [AK_TemplateDocumentDefinition] On [ProofOfConcept].[TemplateDocument] ([TemplateId] ASC, [DefinitionId] ASC, [ObjectId] ASC) WHERE ([DefinitionId] is Not Null)
GO
CREATE INDEX [AK_TemplateDocumentTransform] On [ProofOfConcept].[TemplateDocument] ([TemplateId] ASC, [TransformId] ASC, [ObjectId] ASC) WHERE ([TransformId] is Not Null)
GO
