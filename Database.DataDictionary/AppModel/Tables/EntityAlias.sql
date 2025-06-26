CREATE TABLE [AppModel].[EntityAlias]
(
	[EntityId]          UniqueIdentifier NOT Null,
	[AliasId]           UniqueIdentifier Not Null,
	[AliasScope]        [AppGeneral].[uddtScopeName] NOT NULL,  -- The Scope for the Application to look for the Alias within
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_EntityAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_EntityAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_EntityAlias] PRIMARY KEY CLUSTERED ([EntityId] ASC, [AliasId] ASC),
	CONSTRAINT [FK_EntityAlias_Entity] FOREIGN KEY ([EntityId]) REFERENCES [AppModel].[Entity] ([EntityId]),
	CONSTRAINT [FK_EntityAlias_Alias] FOREIGN KEY ([AliasId]) REFERENCES [AppModel].[AliasNameSpace] ([AliasId]),
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[EntityAlias]))
