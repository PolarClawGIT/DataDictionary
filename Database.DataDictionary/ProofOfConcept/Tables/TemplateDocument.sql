CREATE TABLE [ProofOfConcept].[TemplateDocument]
(	
	[DocumentId]            UniqueIdentifier Not Null CONSTRAINT [DF_DocumentId] DEFAULT (newid()),
	[TemplateId]            UniqueIdentifier Not Null,
	-- Supertype
	-- all Null: (Input, XML, fixed)
	-- [SchemaId] & [ObjectId]: Transform source document (Input, XML)
	-- [TransformId] & [ObjectId]: Transform result (Output)
	-- [TransformId]: Transform result (Output, fixed)
	[SchemaId]				UniqueIdentifier Null,
	[TransformId]			UniqueIdentifier Null,
	[ObjectId]				UniqueIdentifier Null,
	-- File Location
	[RootFolder]			[AppGeneral].[uddtFileRoot] Not Null, -- Name of the Special Folder used as the Root defined in the Application.
	[RelativePath]			[AppGeneral].[uddtFilePath] Null, 
	[FileName]				[AppGeneral].[uddtFileName] Not Null, -- Can be built from Data Object
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateDocument_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateDocument_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_TemplateDocument] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
	CONSTRAINT [FK_TemplateDocumentTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [ProofOfConcept].[Template] ([TemplateId]),
	CONSTRAINT [FK_TemplateDocumentObject] FOREIGN KEY ([TemplateId], [ObjectId]) REFERENCES [ProofOfConcept].[TemplateObject] ([TemplateId], [ObjectId]),
	CONSTRAINT [FK_TemplateDocumentTransform] FOREIGN KEY ([TemplateId], [TransformId]) REFERENCES [ProofOfConcept].[TemplateTransform] ([TemplateId], [TransformId]),
	CONSTRAINT [FK_TemplateDocumentSchema] FOREIGN KEY ([TemplateId], [SchemaId]) REFERENCES [ProofOfConcept].[TemplateSchema] ([TemplateId], [SchemaId]),
	CONSTRAINT [CK_TemplateDocumentType] CHECK (
	([SchemaId] is Null and [TransformId] is Null And [ObjectId] is Null) Or -- Input, Fixed
	([SchemaId] is Not Null and [TransformId] is Null And [ObjectId] is Not Null) Or -- Input, object
	([SchemaId] is Null and [TransformId] is Not Null And [ObjectId] is Not Null) Or -- Output, object
	([SchemaId] is Null and [TransformId] is Not Null And [ObjectId] is Null)), -- Output, Fixed
)
GO
CREATE INDEX [AK_TemplateDocumentDefinition] On [ProofOfConcept].[TemplateDocument] ([TemplateId] ASC, [SchemaId] ASC, [ObjectId] ASC) WHERE ([SchemaId] is Not Null)
GO
CREATE INDEX [AK_TemplateDocumentTransform] On [ProofOfConcept].[TemplateDocument] ([TemplateId] ASC, [TransformId] ASC, [ObjectId] ASC) WHERE ([TransformId] is Not Null)
GO
