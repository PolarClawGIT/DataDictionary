CREATE TABLE [AppModel].[ModelDefinition]
(
	[ModelId]       UniqueIdentifier NOT NULL,
	[DefinitionId]    UniqueIdentifier NOT NULL,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelDefinition_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelDefinition_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelDefinition] PRIMARY KEY ([ModelId] ASC, [DefinitionId] ASC),
	CONSTRAINT [FK_ModelDefinition_Model] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelDefinition_Definition] FOREIGN KEY ([DefinitionId]) REFERENCES [AppModel].[DefinitionEnumeration] ([DefinitionId]),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[ModelDefinition]))