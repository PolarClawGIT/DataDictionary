CREATE TABLE [AppScript].[Document]
(	-- Represents a File in the File System, eiher Input or Output
	[DocumentId]		UniqueIdentifier Not Null CONSTRAINT [DF_DocumentId] DEFAULT (newid()),
	[TemplateId]		UniqueIdentifier Not Null,
	-- Supertype: Input (Schema) or Output (Transform)
	-- [SchemaId] is Not Null: Transform source document (Input)
	-- [TransformId] is Not Null: Transform result (Output)
	[SchemaId]			UniqueIdentifier Null, -- Null = Transform Result/Output, Not Null = XML Input
	[TransformId]		UniqueIdentifier Null, -- Null = XML Input, Not Null = Transform Result/Output
	-- Supertype: Object Content or Fixed Content
	[ObjectId]			UniqueIdentifier Null, -- Null = Fixed Content, Not Null = Object Content
	-- File Location is determined by RootFolder and RelativePath from Transform or Schema
	[FileName]			[AppGeneral].[uddtFileName] Not Null, -- Can be built from Data Object
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Document_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Document_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
	CONSTRAINT [FK_DocumentTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [AppScript].[Template] ([TemplateId]),
	CONSTRAINT [FK_DocumentSchema] FOREIGN KEY ([SchemaId]) REFERENCES [AppScript].[SchemaDefinition] ([SchemaId]),
	CONSTRAINT [FK_DocumentTransform] FOREIGN KEY ([TransformId]) REFERENCES [AppScript].[Transform] ([TransformId]),
	CONSTRAINT [FK_DocumentObject] FOREIGN KEY ([ObjectId]) REFERENCES [AppScript].[DocumentObject] ([ObjectId]),
	CONSTRAINT [CK_TemplateSchema] CHECK (
		([SchemaId] is Not Null And [TransformId] Is Null) Or
		([SchemaId] is Null And [TransformId] is Not Null)),
)	WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsScript].[Document]))
GO
CREATE UNIQUE INDEX [AK_DocumentSchema] ON [AppScript].[Document] ([TemplateId], [SchemaId], [ObjectId]) WHERE ([SchemaId] is Not Null and [ObjectId] is Not Null)
GO
CREATE UNIQUE INDEX [AK_DocumentTransform] ON [AppScript].[Document] ([TemplateId], [TransformId], [ObjectId]) WHERE ([TransformId] is Not Null and [ObjectId] is Not Null)
GO