CREATE TABLE [AppModel].[EntityAttribute]
(
	[EntityAttributeId] UniqueIdentifier Not Null CONSTRAINT [DF_EntityAttributeId] DEFAULT (newid()),
	[EntityId]          UniqueIdentifier Not Null,
	[AttributeAlias]    [App_DataDictionary].[typeTitle] Not Null, -- What to call the Attribute within this Entity (default is the Attribute Name)
	[AttributeName]     [AppModel].[typeQualifiedName] Null, -- What Attribute Name to look for within the same Subject Area/Model to get the definition.
	[OrdinalPosition]   Int Not Null,
	[IsNullable]		Bit Null, -- Is the Attribute Null-able. Overrides the Attribute IsNullable if not null
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_EntityAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_EntityAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_EntityAttribute] PRIMARY KEY CLUSTERED ([EntityAttributeId] ASC),	
	CONSTRAINT [FK_EntityAttribute_Entity] FOREIGN KEY ([EntityId]) REFERENCES [AppModel].[Entity] ([EntityId]),
	CONSTRAINT [AK_EntityAttribute] UNIQUE ([EntityId] ASC, [OrdinalPosition] ASC),
	CONSTRAINT [AK_EntityAttributeName] UNIQUE ([EntityId] ASC, [AttributeAlias] ASC),
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[EntityAttribute]))
