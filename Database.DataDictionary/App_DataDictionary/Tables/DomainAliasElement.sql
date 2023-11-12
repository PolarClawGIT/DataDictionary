CREATE TABLE [App_DataDictionary].[DomainAliasElement]
(	-- Because of SQL Indexing limit, the Unique Key ([AliasElementName], [AliasElementParentId]) cannot be enforced.
	[AliasElementId]          UniqueIdentifier Not Null CONSTRAINT [DF_DomainAliasElementId] DEFAULT (newsequentialid()),
	[AliasElementParentId]    UniqueIdentifier Null,
	[AliasId]                 UniqueIdentifier Not Null,
	[AliasElementName]        [App_DataDictionary].[typeNameSpaceElement] Not Null,
	[ScopeId]                 Int NOT Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_Alias_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAliasElement_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAliasElement_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainAliasElement] PRIMARY KEY CLUSTERED ([AliasElementId] ASC),
	CONSTRAINT [UK_DomainAliasElement] UNIQUE ([AliasElementId] ASC, [AliasId]),
	CONSTRAINT [FK_DomainAliasElementParent] FOREIGN KEY ([AliasElementParentId], [AliasId]) REFERENCES [App_DataDictionary].[DomainAliasElement] ([AliasElementId], [AliasId]),
	CONSTRAINT [FK_DomainAliasElementScope] FOREIGN KEY ([ScopeId]) REFERENCES [App_DataDictionary].[ApplicationScope] ([ScopeId]),
)
GO

