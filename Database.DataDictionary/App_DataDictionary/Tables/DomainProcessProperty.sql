CREATE TABLE [App_DataDictionary].[DomainProcessProperty]
(
	[ProcessId]			UniqueIdentifier Not Null,
	[PropertyId]		UniqueIdentifier NOT Null,
	[PropertyValue]		NVarChar(4000) Null, -- The Value for the Property. (Summary Text, Extended Property, Choice)
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy]			SysName Not Null CONSTRAINT [DF_DomainProcessProperty_ModifiedBy] DEFAULT (original_login()),
	[SysStart]			DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainProcessProperty_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]			DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainProcessProperty_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainProcessProperty] PRIMARY KEY CLUSTERED ([ProcessId] ASC, [PropertyId] ASC),
	CONSTRAINT [FK_DomainProcessPropertyDomainProcess] FOREIGN KEY ([ProcessId]) REFERENCES [App_DataDictionary].[DomainProcess] ([ProcessId]),
	CONSTRAINT [FK_DomainProcessPropertyApplicationProperty] FOREIGN KEY ([PropertyId]) REFERENCES [App_DataDictionary].[DomainProperty] ([PropertyId]),
)