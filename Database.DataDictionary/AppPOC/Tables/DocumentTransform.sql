CREATE TABLE [AppPOC].[DocumentTransform]
(	-- Output Document, Sub-Type
	[DocumentId]            UniqueIdentifier Not Null,
	[TemplateId]            UniqueIdentifier Not Null,
	[TransformId]			UniqueIdentifier Not Null,
	[ObjectId]				UniqueIdentifier Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DocumentTransform_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DocumentTransform_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DocumentTransform] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [DocumentId] ASC),
	CONSTRAINT [FK_DocumentTransformTemplate] FOREIGN KEY ([TemplateId], [TransformId]) REFERENCES [AppPOC].[TemplateTransform] ([TemplateId], [TransformId]),
	CONSTRAINT [FK_DocumentTransformDocument] FOREIGN KEY ([DocumentId]) REFERENCES [AppPOC].[Document] ([DocumentId]),
	CONSTRAINT [FK_DocumentTransformObject] FOREIGN KEY ([TemplateId], [ObjectId]) REFERENCES [AppPOC].[TemplateObject] ([TemplateId], [ObjectId]),
)
GO
CREATE UNIQUE INDEX [AK_DocumentTransformObject] ON [AppPOC].[DocumentTransform] ([TemplateId] ASC, [TransformId] ASC, [ObjectId] ASC) WHERE ([ObjectId] is Not Null)
GO