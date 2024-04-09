CREATE TABLE [App_DataDictionary].[DomainAlias]
(
	-- Domain Alias contains only the name structure.
	-- Multiple items may have the same Alias Name but refer to different objects.
	[AliasId]           UniqueIdentifier NOT NULL CONSTRAINT [DF_DomainAliasId] DEFAULT (newid()),
	[ParentAliasId]     UniqueIdentifier NULL,
	[AliasMember]       [App_DataDictionary].[typeNameSpaceMember] Not Null,
	[ScopeName]         [App_DataDictionary].[typeScopeName] Null,  -- The Scope for the Application to look for the Alias within
	--[AliasCheckSum]     As (Binary_CheckSum([AliasMember])), -- Used for indexing. Not needed unless the index would exceed 1700 bytes.
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainAlias_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_DomainAlias] PRIMARY KEY CLUSTERED ([AliasId] ASC),
	CONSTRAINT [FK_DomainAliasParent] FOREIGN KEY ([ParentAliasId]) REFERENCES [App_DataDictionary].[DomainAlias] ([AliasId]),
)
GO
CREATE UNIQUE INDEX [AK_DomainAlias]
    ON [App_DataDictionary].[DomainAlias]([ParentAliasId] ASC, [AliasMember] ASC)
GO