CREATE TABLE [App_DataDictionary].[DomainAttributeAlias]
(
	[AttributeId]      UniqueIdentifier Not Null,
	[AliasId]          UniqueIdentifier Not Null,
	[ScopeId]          Int NOT Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainAttributeAlias_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAttributeAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAttributeAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainAttributeAlias] PRIMARY KEY CLUSTERED ([AttributeId] ASC, [AliasId] ASC),
	CONSTRAINT [FK_DomainAttributeAlias] FOREIGN KEY ([AliasId]) REFERENCES [App_DataDictionary].[DomainAlias] ([AliasId]),
	CONSTRAINT [FK_DomainAttributeAliasScope] FOREIGN KEY ([ScopeId]) REFERENCES [App_DataDictionary].[ApplicationScope] ([ScopeId]),
)
GO

