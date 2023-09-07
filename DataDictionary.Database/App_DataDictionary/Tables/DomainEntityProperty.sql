CREATE TABLE [App_DataDictionary].[DomainEntityProperty]
(
	-- To be implemented later.
	[EntityId]      UniqueIdentifier Not Null,
	[PropertyId]    UniqueIdentifier NOT Null,
	-- This is a Sub-Type based on what the Property Type defines.
		-- Definition Sub-Type
		[DefinitionText]          NVarChar(Max) Null, -- Contains Rich Text Definition
		-- Extended Property Sub-Type
		[ExtendedPropertyValue]   NVarChar(4000) Null, -- Only supporting SQLVarent as character data
		-- Choice Property Sub-Type
		[ChoiceValue]             NVarChar(50) Null, -- Choice Selection
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy]     SysName Not Null CONSTRAINT [DF_DomainEntityProperty_ModfiedBy] DEFAULT (original_login()),
	[SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainEntityProperty_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainEntityProperty_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainEntityProperty] PRIMARY KEY CLUSTERED ([EntityId] ASC, [PropertyId] ASC),
	CONSTRAINT [FK_DomainEntityPropertyDomainEntity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
	CONSTRAINT [FK_DomainEntityPropertyApplicationProperty] FOREIGN KEY ([PropertyId]) REFERENCES [App_DataDictionary].[ApplicationProperty] ([PropertyId]),
)
