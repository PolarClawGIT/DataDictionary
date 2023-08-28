CREATE TABLE [App_DataDictionary].[DomainAttributeDefinition]
(
	[AttributeId]    UniqueIdentifier NOT NULL,
	[DefinitionId]   UniqueIdentifier NOT NULL,
	[DefinitionText] NVarChar(Max) NULL,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy]     SysName Not Null CONSTRAINT [DF_DomainAttributeDefinition_ModfiedBy] DEFAULT (original_login()),
	[SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAttributeDefinition_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAttributeDefinition_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainAttributeDefinition] PRIMARY KEY CLUSTERED ([AttributeId] ASC, [DefinitionId] ASC),
	CONSTRAINT [FK_DomainAttributeDefinitionDomainAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
	CONSTRAINT [FK_DomainAttributeDefinitionApplicationDefinition] FOREIGN KEY ([DefinitionId]) REFERENCES [App_DataDictionary].[ApplicationDefinition] ([DefinitionId]),

)
