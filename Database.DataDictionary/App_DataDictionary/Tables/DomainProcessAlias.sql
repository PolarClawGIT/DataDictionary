CREATE TABLE [App_DataDictionary].[DomainProcessAlias]
(
	[ProcessId]         UniqueIdentifier Not Null,
	[NameSpaceId]       UniqueIdentifier NOT NULL,
	[AliasScope]        [App_DataDictionary].[typeScopeName] NULL,  -- The Scope for the Application to look for the Alias within
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_DomainProcessAlias_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainProcessAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainProcessAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_DomainProcessAlias] PRIMARY KEY CLUSTERED ([ProcessId] ASC, [NameSpaceId] ASC),
	CONSTRAINT [FK_DomainProcessAlias_Process] FOREIGN KEY ([ProcessId]) REFERENCES [App_DataDictionary].[DomainProcess] ([ProcessId]),
	CONSTRAINT [FK_DomainProcessAlias_NameSpace] FOREIGN KEY ([NameSpaceId]) REFERENCES [App_DataDictionary].[ModelNameSpace] ([NameSpaceId]),
)

