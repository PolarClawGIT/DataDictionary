CREATE TABLE [Obsolete].[ScriptingModel]
(
	[ModelId]		UniqueIdentifier NOT NULL,
	[TemplateId]	UniqueIdentifier NULL,
	[DataSourceId]	UniqueIdentifier NULL,
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelScripting_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelScripting_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelScripting] UNIQUE CLUSTERED ([ModelId] ASC, [TemplateId] ASC, [DataSourceId] ASC),
	CONSTRAINT [FK_ModelScriptingModel] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [Obsolete].[Template] ([TemplateId]),
	CONSTRAINT [FK_ModelDataSource] FOREIGN KEY ([DataSourceId]) REFERENCES [Obsolete].[DataSource] ([DataSourceId]),
)
GO
