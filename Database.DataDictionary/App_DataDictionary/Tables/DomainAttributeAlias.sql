CREATE TABLE [App_DataDictionary].[DomainAttributeAlias]
(
	[AttributeId]      UniqueIdentifier Not Null,
	[AliasElementId]   UniqueIdentifier Not Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainAttributeAlias_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAttributeAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAttributeAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainAttributeAlias] PRIMARY KEY CLUSTERED ([AttributeId] ASC, [AliasElementId] ASC),
	CONSTRAINT [FK_DomainAttributeAlias] FOREIGN KEY ([AliasElementId]) REFERENCES [App_DataDictionary].[DomainAliasElement] ([AliasElementId]),
)
GO

