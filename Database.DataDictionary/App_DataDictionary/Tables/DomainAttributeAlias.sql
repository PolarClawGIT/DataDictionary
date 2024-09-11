CREATE TABLE [App_DataDictionary].[DomainAttributeAlias]
(
	[AttributeId]       UniqueIdentifier NOT Null,
	[NameSpaceId]       UniqueIdentifier NOT NULL,
	[AliasScope]        [App_DataDictionary].[typeScopeName] NOT NULL,  -- The Scope for the Application to look for the Alias within
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_DomainAttributeAlias_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAttributeAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAttributeAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_DomainAttributeAlias] PRIMARY KEY CLUSTERED ([AttributeId] ASC, [NameSpaceId] ASC),
--	CONSTRAINT [FK_DomainAttributeAlias_Alias] FOREIGN KEY ([AliasId]) REFERENCES [App_DataDictionary].[DomainAlias] ([AliasId]),
	CONSTRAINT [FK_DomainAttributeAlias_Attribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
	CONSTRAINT [FK_DomainAttributeAlias_NameSpace] FOREIGN KEY ([NameSpaceId]) REFERENCES [App_DataDictionary].[ModelNameSpace] ([NameSpaceId]),
)
