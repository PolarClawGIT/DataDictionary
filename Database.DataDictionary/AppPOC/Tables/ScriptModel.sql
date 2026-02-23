CREATE TABLE [AppPOC].[ScriptModel]
(	-- Sub-Type structure. Type is dependent on [TemplateId], [DataId], [DocumentId], or [TransformId] being not null.
	[ModelId]			UniqueIdentifier Not Null,
	[TemplateId]		UniqueIdentifier Null,
	[DataId]			UniqueIdentifier Null,
	[TransformId]		UniqueIdentifier Null,
	[DocumentId]        UniqueIdentifier Null,
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ScriptModel_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ScriptModel_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ScriptModel] UNIQUE CLUSTERED ([ModelId] ASC, [TemplateId] ASC, [DataId] ASC, [TransformId] ASC),
	CONSTRAINT [FK_ScriptModelData] FOREIGN KEY ([DataId]) REFERENCES [AppPOC].[DataDefinition] ([DataId]),
	CONSTRAINT [FK_ScriptModelTransform] FOREIGN KEY ([TransformId]) REFERENCES [AppPOC].[Transform] ([TransformId]),
	CONSTRAINT [FK_ScriptModelTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [AppPOC].[Template] ([TemplateId]),
	CONSTRAINT [FK_ScriptModelDocument] FOREIGN KEY ([DocumentId]) REFERENCES [AppPOC].[Document] ([DocumentId]),

	CONSTRAINT [CK_ScriptModelType] CHECK (
		([TemplateId] is Not Null And [DataId] is Null And [TransformId] is Null And [DocumentId] is Null) And 
		([TemplateId] is Null And [DataId] is Not Null And [TransformId] is Null And [DocumentId] is Null) And 
		([TemplateId] is Null And [DataId] is Null And [TransformId] is Not Null And [DocumentId] is Null) And
		([TemplateId] is Null And [DataId] is Null And [TransformId] is Null And [DocumentId] is Not Null)),
)
GO
CREATE INDEX [AK_ScriptModelData] On [AppPOC].[ScriptModel] ([ModelId] ASC, [DataId] ASC) WHERE ([DataId] is Not Null)
GO
CREATE INDEX [AK_ScriptModelTransform] On [AppPOC].[ScriptModel] ([ModelId] ASC, [TransformId] ASC) WHERE ([TransformId] is Not Null)
GO
CREATE INDEX [AK_ScriptModelTemplate] On [AppPOC].[ScriptModel] ([ModelId] ASC, [TemplateId] ASC) WHERE ([TemplateId] is Not Null)
GO
CREATE INDEX [AK_ScriptModelDocument] On [AppPOC].[ScriptModel] ([ModelId] ASC, [DocumentId] ASC) WHERE ([DocumentId] is Not Null)
GO