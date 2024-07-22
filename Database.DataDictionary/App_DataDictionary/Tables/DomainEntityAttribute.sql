CREATE TABLE [App_DataDictionary].[DomainEntityAttribute]
(
	[EntityId]          UniqueIdentifier Not Null,
	[AttributeId]       UniqueIdentifier Not Null,
	[OrdinalPosition]   Int Not Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainEntityAttribute_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainEntityAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainEntityAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainEntityAttribute] PRIMARY KEY CLUSTERED ([EntityId] ASC, [AttributeId] ASC),	
	CONSTRAINT [FK_DomainEntityAttribute_Attribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
	CONSTRAINT [FK_DomainEntityAttribute_Entity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
	CONSTRAINT [AK_DomainEntityAttribute] UNIQUE ([EntityId] ASC, [OrdinalPosition] ASC),
)
