CREATE TABLE [AppModel].[EntityAlias]
(
	[EntityId]          UniqueIdentifier NOT Null,
	[NameSpaceId]       UniqueIdentifier NOT NULL,
	[AliasScope]        [App_DataDictionary].[typeScopeName] NOT NULL,  -- The Scope for the Application to look for the Alias within
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_EntityAlias_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_EntityAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_EntityAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_EntityAlias] PRIMARY KEY CLUSTERED ([EntityId] ASC, [NameSpaceId] ASC),
--	CONSTRAINT [FK_EntityAlias_Alias] FOREIGN KEY ([AliasId]) REFERENCES [App_DataDictionary].[DomainAlias] ([AliasId]),
	CONSTRAINT [FK_EntityAlias_Entity] FOREIGN KEY ([EntityId]) REFERENCES [AppModel].[Entity] ([EntityId]),
	CONSTRAINT [FK_EntityAlias_NameSpace] FOREIGN KEY ([NameSpaceId]) REFERENCES [AppModel].[NameSpaceHierarchy] ([NameSpaceId]),
)
