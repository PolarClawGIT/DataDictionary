CREATE TABLE [App_DataDictionary].[DomainEntityAlias]
( 
	[EntityId]         UniqueIdentifier Not Null,
	[AliasElementId]   UniqueIdentifier Not Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainEntityAlias_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainEntityAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainEntityAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainEntityAlias] PRIMARY KEY CLUSTERED ([EntityId] ASC, [AliasElementId] ASC),
	CONSTRAINT [FK_DomainEntityAlias] FOREIGN KEY ([AliasElementId]) REFERENCES [App_DataDictionary].[DomainAliasElement] ([AliasElementId]),
)
GO
