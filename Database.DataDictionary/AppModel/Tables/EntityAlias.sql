CREATE TABLE [AppModel].[EntityAlias]
(
	[EntityId]          UniqueIdentifier NOT Null,
	[AliasId]           UniqueIdentifier Not Null,
	[AliasScope]        [AppModel].[typeScopeName] NOT NULL,  -- The Scope for the Application to look for the Alias within
--	[AliasNameSpace]    [App_DataDictionary].[typeNameSpacePath] Null, -- Delimited NameSpace. Cannot be indexed.
	-- Relationship Options (based on Object Oriented programing)
	-- [IsInheritance]		Bit Not Null CONSTRAINT [Df_EntityAlias_IsInheritance] DEFAULT (0), -- Is this Entity Inherited from the specified object. IE: Business layer Inherits from the Data Layer.
	-- [IsAbstraction]		Bit Not Null CONSTRAINT [Df_EntityAlias_IsAbstraction] DEFAULT (0), -- Is this Entity an Abstraction of the specified object. IE: DataLayer Abstracts the Db Table.
	-- [IsEncapsulation]	Bit Not Null CONSTRAINT [Df_EntityAlias_IsEncapsulation] DEFAULT (0), -- Does the Entity Encapsulate the specified object. IE: Combines Data and Methods to create an Object.
	-- [IsAssociation]		Bit Not Null CONSTRAINT [Df_EntityAlias_IsAssociation] DEFAULT (0), -- Is this Entity used with (associated) the specified object. IE: Makes use of
	-- [IsComposition]		Bit Not Null CONSTRAINT [Df_EntityAlias_IsComposition] DEFAULT (0), -- Is this Entity a Composition of the specified object. IE: Combines with other objects to create a more complex object.
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_EntityAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_EntityAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_EntityAlias] PRIMARY KEY CLUSTERED ([EntityId] ASC, [AliasId] ASC),
	CONSTRAINT [FK_EntityAlias_Entity] FOREIGN KEY ([EntityId]) REFERENCES [AppModel].[Entity] ([EntityId]),
	CONSTRAINT [FK_EntityAlias_Alias] FOREIGN KEY ([AliasId]) REFERENCES [AppModel].[AliasHierarchy] ([AliasId]),
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[EntityAlias]))
