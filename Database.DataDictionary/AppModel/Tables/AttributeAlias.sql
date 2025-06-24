CREATE TABLE [AppModel].[AttributeAlias]
(
	[AttributeId]       UniqueIdentifier NOT Null,
	[AliasId]           UniqueIdentifier Not Null,
	[AliasScope]        [AppGeneral].[uddtScopeName] NOT NULL,  -- The Scope for the Application to look for the Alias within
	--[AliasNameSpace]    [App_DataDictionary].[typeNameSpacePath] Null, -- Delimited NameSpace. Cannot be indexed.
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_AttributeAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_AttributeAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_AttributeAlias] PRIMARY KEY CLUSTERED ([AttributeId] ASC, [AliasId] ASC),
	CONSTRAINT [FK_AttributeAlias_Attribute] FOREIGN KEY ([AttributeId]) REFERENCES [AppModel].[Attribute] ([AttributeId]),
	CONSTRAINT [FK_AttributeAlias_Alias] FOREIGN KEY ([AliasId]) REFERENCES [AppModel].[AliasNameSpace] ([AliasId]),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[AttributeAlias]))
GO