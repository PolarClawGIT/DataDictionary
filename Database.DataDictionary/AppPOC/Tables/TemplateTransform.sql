CREATE TABLE [AppPOC].[TemplateTransform]
(	-- Ties the Template to the Transform and the Data
	[TemplateId]            UniqueIdentifier Not Null,
	[TransformId]			UniqueIdentifier Not Null,
	[DefinitionId]			UniqueIdentifier Not Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateTransform_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateTransform_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_TemplateTransform] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [TransformId] ASC),  -- FK on Transform
	CONSTRAINT [AK_TemplateTransformDefinition] UNIQUE ([TemplateId] ASC, [DefinitionId] ASC), -- FK on Data
	CONSTRAINT [FK_TemplateTransformTransform] FOREIGN KEY ([TransformId]) REFERENCES [AppPOC].[TransformDefinition] ([TransformId]),
	CONSTRAINT [FK_TemplateTransformTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [AppPOC].[Template] ([TemplateId]),
	CONSTRAINT [FK_TemplateTransformDefinition] FOREIGN KEY ([DefinitionId]) REFERENCES [AppPOC].[SchemaDefinition] ([DefinitionId]),
)
