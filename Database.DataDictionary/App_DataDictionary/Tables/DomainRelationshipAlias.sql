CREATE TABLE [App_DataDictionary].[DomainRelationshipAlias]
(
	[RelationshipId]    UniqueIdentifier Not Null,
	[NameSpaceId]       UniqueIdentifier NOT NULL,
	[AliasScope]        [App_DataDictionary].[typeScopeName] NULL,  -- The Scope for the Application to look for the Alias within
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainRelationshipAlias_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainRelationshipAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainRelationshipAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_DomainRelationshipAlias] PRIMARY KEY CLUSTERED ([RelationshipId] ASC, [NameSpaceId] ASC),
	CONSTRAINT [FK_DomainRelationshipAlias_Relationship] FOREIGN KEY ([RelationshipId]) REFERENCES [App_DataDictionary].[DomainRelationship] ([RelationshipId]),
	CONSTRAINT [FK_DomainRelationshipAlias_NameSpace] FOREIGN KEY ([NameSpaceId]) REFERENCES [App_DataDictionary].[ModelNameSpace] ([NameSpaceId]),
)
