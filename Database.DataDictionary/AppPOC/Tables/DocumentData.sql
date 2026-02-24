CREATE TABLE [AppPOC].[DocumentData]
(	-- Input Document, Sub-Type
	[DocumentId]            UniqueIdentifier Not Null,
	[TemplateId]            UniqueIdentifier Not Null,
	[DataId]				UniqueIdentifier Null,
	[ObjectId]				UniqueIdentifier Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DocumentData_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DocumentData_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DocumentData] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [DocumentId] ASC),
	--CONSTRAINT [AK_DocumentDataObject] UNIQUE ([TemplateId] ASC, [DataId] ASC, [ObjectId] ASC),
	CONSTRAINT [FK_DocumentDataTemplate] FOREIGN KEY ([TemplateId], [DataId]) REFERENCES [AppPOC].[TemplateTransform] ([TemplateId], [DataId]),
	CONSTRAINT [FK_DocumentDataDocument] FOREIGN KEY ([DocumentId]) REFERENCES [AppPOC].[Document] ([DocumentId]),
	CONSTRAINT [FK_DocumentDataObject] FOREIGN KEY ([TemplateId], [ObjectId]) REFERENCES [AppPOC].[TemplateObject] ([TemplateId], [ObjectId]),
)
GO
CREATE UNIQUE INDEX [AK_DocumentDataObject] ON [AppPOC].[DocumentData] ([TemplateId] ASC, [DataId] ASC, [ObjectId] ASC) WHERE ([ObjectId] is Not Null)
GO
