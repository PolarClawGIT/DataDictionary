CREATE TABLE [App_DataDictionary].[ModelProcess]
(
	[ModelId] UniqueIdentifier NOT NULL,
	[ProcessId] UniqueIdentifier NOT NULL,
	[SubjectAreaId] UniqueIdentifier Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ModelProcess_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelProcess_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelProcess_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelProcess] PRIMARY KEY CLUSTERED ([ModelId] ASC, [ProcessId] ASC),
	CONSTRAINT [FK_ModelProcess_Model] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelDomainProcess] FOREIGN KEY ([ProcessId]) REFERENCES [App_DataDictionary].[DomainProcess] ([ProcessId]),
	CONSTRAINT [FK_ModelSubjectAreaProcess] FOREIGN KEY ([ModelId], [SubjectAreaId]) REFERENCES [App_DataDictionary].[ModelSubjectArea] ([ModelId], [SubjectAreaId]),

)
