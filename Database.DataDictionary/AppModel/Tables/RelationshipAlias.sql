CREATE TABLE [AppModel].[RelationshipAlias]
(
	[AliasId]           UniqueIdentifier Not Null CONSTRAINT [DF_RelationshipAliasId] DEFAULT (newid()),
	[RelationshipId]    UniqueIdentifier Not Null,
	[AliasScope]        [App_DataDictionary].[typeScopeName] NOT NULL,  -- The Scope for the Application to look for the Alias within
	[AliasNameSpace]    [App_DataDictionary].[typeNameSpacePath] Null, -- Delimited NameSpace. Cannot be indexed.
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_RelationshipAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_RelationshipAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_RelationshipAlias] PRIMARY KEY CLUSTERED ([AliasId] ASC),
	CONSTRAINT [FK_RelationshipAlias_Relationship] FOREIGN KEY ([RelationshipId]) REFERENCES [AppModel].[Relationship] ([RelationshipId]),
)
