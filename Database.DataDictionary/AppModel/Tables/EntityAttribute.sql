CREATE TABLE [AppModel].[EntityAttribute]
(
	[EntityAttributeId] UniqueIdentifier Not Null CONSTRAINT [DF_EntityAttributeId] DEFAULT (newid()),
	[EntityId]          UniqueIdentifier Not Null,
	[AttributeId]       UniqueIdentifier Not Null, -- Same Attribute can appear multiple times but they must have a Name to do so
	[AttributeName]     [App_DataDictionary].[typeTitle] Null, -- Name the Entity knows the attribute as
	[IsNullable]		Bit Null, -- Is the Attribute Null-able. Overrides the Attribute IsNullable if not null
	[OrdinalPosition]   Int Not Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_EntityAttribute_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_EntityAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_EntityAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_EntityAttribute] PRIMARY KEY CLUSTERED ([EntityAttributeId] ASC),	
	CONSTRAINT [FK_EntityAttribute_Attribute] FOREIGN KEY ([AttributeId]) REFERENCES [AppModel].[Attribute] ([AttributeId]),
	CONSTRAINT [FK_EntityAttribute_Entity] FOREIGN KEY ([EntityId]) REFERENCES [AppModel].[Entity] ([EntityId]),
	CONSTRAINT [AK_EntityAttribute] UNIQUE ([EntityId] ASC, [OrdinalPosition] ASC),
)
