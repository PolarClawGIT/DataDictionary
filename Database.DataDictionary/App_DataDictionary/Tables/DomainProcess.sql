CREATE TABLE [App_DataDictionary].[DomainProcess]
(
	[ProcessId] UniqueIdentifier Not Null CONSTRAINT [DF_DomainProcessId] DEFAULT (newid()),
	[ProcessTitle] [App_DataDictionary].[typeTitle] Not Null,
	[ProcessDescription] [App_DataDictionary].[typeDescription] Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainProcess_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainProcess_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainProcess_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainProcess] PRIMARY KEY CLUSTERED ([ProcessId] ASC),	

)
