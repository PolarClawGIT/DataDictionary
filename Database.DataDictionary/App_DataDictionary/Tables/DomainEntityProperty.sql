CREATE TABLE [App_DataDictionary].[DomainEntityProperty]
(
	[EntityId]			UniqueIdentifier Not Null,
	[PropertyId]		UniqueIdentifier NOT Null,
	[PropertyValue]		NVarChar(4000) Null, -- The Value for the Property. (Summary Text, Extended Property, Choice)
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy]			SysName Not Null CONSTRAINT [DF_DomainEntityProperty_ModifiedBy] DEFAULT (original_login()),
	[SysStart]			DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainEntityProperty_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]			DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainEntityProperty_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainEntityProperty] PRIMARY KEY CLUSTERED ([EntityId] ASC, [PropertyId] ASC),
	CONSTRAINT [FK_DomainEntityPropertyDomainEntity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
	CONSTRAINT [FK_DomainEntityPropertyApplicationProperty] FOREIGN KEY ([PropertyId]) REFERENCES [App_DataDictionary].[DomainProperty] ([PropertyId]),
)
