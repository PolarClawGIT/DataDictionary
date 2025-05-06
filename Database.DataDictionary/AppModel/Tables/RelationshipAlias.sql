CREATE TABLE [AppModel].[RelationshipAlias]
(
	[RelationshipId]    UniqueIdentifier Not Null,
	[AliasId]           UniqueIdentifier Not Null,
	[AliasScope]        [AppModel].[typeScopeName] NOT NULL,  -- The Scope for the Application to look for the Alias within
    -- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_RelationshipAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_RelationshipAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_RelationshipAlias] PRIMARY KEY CLUSTERED ([RelationshipId] ASC, [AliasId] ASC),
	CONSTRAINT [FK_RelationshipAlias_Relationship] FOREIGN KEY ([RelationshipId]) REFERENCES [AppModel].[Relationship] ([RelationshipId]),
	CONSTRAINT [FK_RelationshipAlias_Alias] FOREIGN KEY ([AliasId]) REFERENCES [AppModel].[AliasHierarchy] ([AliasId]),
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[RelationshipAlias]))
GO
