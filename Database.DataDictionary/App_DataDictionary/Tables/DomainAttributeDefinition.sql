CREATE TABLE [App_DataDictionary].[DomainAttributeDefinition]
(
	[AttributeId]       UniqueIdentifier NOT Null,
	[DefinitionId]      UniqueIdentifier NOT NULL,
	[DefinitionSummary] [App_DataDictionary].[typeDescription] Null, -- Plain Text summary, used where RTF cannot be used.
	[DefinitionText]    [App_DataDictionary].[typeRichText] Null, -- Contains Rich Text Definition. Rich Text must be handled differently.
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainAttributeDefinitionModfiedBy] DEFAULT (ORIGINAL_LOGIN()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_DomainAttributeDefinition_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_DomainAttributeDefinition_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainAttributeDefinition] PRIMARY KEY CLUSTERED ([AttributeId] ASC, [DefinitionId] ASC),
	CONSTRAINT [FK_DomainAttributeDefinitionAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
	CONSTRAINT [FK_DomainAttributeDefinitionDefinition] FOREIGN KEY ([DefinitionId]) REFERENCES [App_DataDictionary].[DomainDefinition] ([DefinitionId]),
)
