CREATE TABLE [App_DataDictionary].[DomainAlias]
(
	[AliasId]           UniqueIdentifier NOT NULL CONSTRAINT [DF_DomainAliasId] DEFAULT (newid()),
	[ParentAliasId]     UniqueIdentifier NULL,
	[AliasSourceId]     UniqueIdentifier NOT NULL,
	[AliasElement]      [App_DataDictionary].[typeAliasElement] Not Null, -- Cannot enforce unique value because of Index limits.
	--[AliasCheckSum]     As (Binary_CheckSum([AliasNameElement])), -- Used for case Sensitive and indexing
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainAlias_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_DomainAlias] PRIMARY KEY CLUSTERED ([AliasId] ASC),
	CONSTRAINT [FK_DomainAliasParent] FOREIGN KEY ([ParentAliasId]) REFERENCES [App_DataDictionary].[DomainAlias] ([AliasId]),
	CONSTRAINT [FK_DomainAliasSource] FOREIGN KEY ([AliasSourceId]) REFERENCES [App_DataDictionary].[DomainSource] ([SourceId]),
)
