CREATE TABLE [App_DataDictionary].[ModelDefinition]
(
	[ModelId]       UniqueIdentifier NOT NULL,
	[DefinitionId]    UniqueIdentifier NOT NULL,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName NOT NULL CONSTRAINT [DF_ModelDefinition_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelDefinition_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelDefinition_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelDefinition] PRIMARY KEY ([ModelId] ASC, [DefinitionId] ASC),
	CONSTRAINT [FK_ModelDefinition_Model] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelDefinition_Definition] FOREIGN KEY ([DefinitionId]) REFERENCES [App_DataDictionary].[DomainDefinition] ([DefinitionId]),
)
GO
