CREATE TABLE [AppModel].[EntityAttribute]
(
	[EntityId]          UniqueIdentifier Not Null,
	[AttributeAliasId]  UniqueIdentifier Not Null,
	[AttributeKnownAs]	[App_DataDictionary].[typeTitle] Not Null, -- What to call the Attribute within this Entity (default is the Attribute Name)
	[OrdinalPosition]   Int Not Null,
	[IsNullable]		Bit Not Null CONSTRAINT [DF_EntityAttributeNullable] DEFAULT (0), -- Is the Attribute Null-able.
	[IsPrimaryKey]		Bit Not Null CONSTRAINT [DF_EntityAttributePrimaryKey] DEFAULT (0), -- Is the Attribute a Primary key of the Entity  
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_EntityAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_EntityAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_EntityAttribute] PRIMARY KEY CLUSTERED ([EntityId] ASC, [AttributeAliasId] ASC),	
	CONSTRAINT [FK_EntityAttribute_Entity] FOREIGN KEY ([EntityId]) REFERENCES [AppModel].[Entity] ([EntityId]),
	CONSTRAINT [FK_EntityAttribute_Alias] FOREIGN KEY ([AttributeAliasId]) REFERENCES [AppModel].[AliasHierarchy] ([AliasId]),
	CONSTRAINT [AK_EntityAttributePosition] UNIQUE ([EntityId] ASC, [OrdinalPosition] ASC),
	CONSTRAINT [AK_EntityAttributeTitle] UNIQUE ([EntityId] ASC, [AttributeKnownAs] ASC),
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[EntityAttribute]))
