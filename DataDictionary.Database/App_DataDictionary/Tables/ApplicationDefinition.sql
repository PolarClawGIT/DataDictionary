CREATE TABLE [App_DataDictionary].[ApplicationDefinition]
(
	[DefinitionId] UniqueIdentifier NOT NULL CONSTRAINT [DF_ApplicationDefinitionDefinitionId] DEFAULT (newid()),
	[DefinitionTitle] [App_DataDictionary].[typeTitle] Not Null,
	[Obsolete] As (CONVERT([bit],case when [ObsoleteDate] IS NULL then (0) else (1) end)),
	-- TODO: Add System Version later once the schema is locked down
	[ObsoleteDate] DATETIME2 Null, -- Used to flag an item as a candidate for being deleted. Null = active, anything else is Obsolete.
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ApplicationDefinition_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ApplicationDefinition_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ApplicationDefinition_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Definition] PRIMARY KEY CLUSTERED ([DefinitionId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Definition]
    ON [App_DataDictionary].[ApplicationDefinition]([DefinitionTitle] ASC, [ObsoleteDate] ASC);
GO