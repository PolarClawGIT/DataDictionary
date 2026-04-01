CREATE TABLE [ProofOfConcept].[ScriptModel]
(
	[ModelId]			UniqueIdentifier Not Null,
	-- Type is dependent on [TemplateId], [DataId], [DocumentId], or [TransformId] being not null.
	[TemplateId]		UniqueIdentifier Not Null,
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ScriptModel_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ScriptModel_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ScriptModel] PRIMARY KEY CLUSTERED ([ModelId] ASC, [TemplateId] ASC),
	CONSTRAINT [FK_ScriptModelModel] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
	CONSTRAINT [FK_ScriptModelTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [ProofOfConcept].[Template] ([TemplateId]),
)
