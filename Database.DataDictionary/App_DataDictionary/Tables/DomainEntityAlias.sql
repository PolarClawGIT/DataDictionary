CREATE TABLE [App_DataDictionary].[DomainEntityAlias]
(
	[EntityId]          UniqueIdentifier NOT Null,
	[NameSpaceId]       UniqueIdentifier NOT NULL,
	[AliasScope]        [App_DataDictionary].[typeScopeName] NOT NULL,  -- The Scope for the Application to look for the Alias within
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_DomainEntityAlias_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainEntityAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainEntityAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_DomainEntityAlias] PRIMARY KEY CLUSTERED ([EntityId] ASC, [NameSpaceId] ASC),
--	CONSTRAINT [FK_DomainEntityAlias_Alias] FOREIGN KEY ([AliasId]) REFERENCES [App_DataDictionary].[DomainAlias] ([AliasId]),
	CONSTRAINT [FK_DomainEntityAlias_Entity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
	CONSTRAINT [FK_DomainEntityAlias_NameSpace] FOREIGN KEY ([NameSpaceId]) REFERENCES [App_DataDictionary].[ModelNameSpace] ([NameSpaceId]),
)
