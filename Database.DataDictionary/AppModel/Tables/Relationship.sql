CREATE TABLE [AppModel].[Relationship]
(
	[RelationshipId]          UniqueIdentifier Not Null CONSTRAINT [DF_RelationshipId] DEFAULT (newid()),
	[RelationshipTitle]       [App_DataDictionary].[typeTitle] Not Null,
	[RelationshipDescription] [App_DataDictionary].[typeDescription] Null,
	[EntityId]                UniqueIdentifier Not Null, -- Owner Entity
	[RelationshipType]        NVarChar(20) Not Null, -- Type of Relationship
		-- Primary Key: A Compounded Attribute of an Entity that identifies that Entity.
		-- Unique Key: A Compounded Attribute of an Entity that is a Unique to that Entity.
		-- Inversion Entry: A Compounded Attribute of an Entity that is used as a key.
		-- [Currently Not handled]
		-- Foreign Key: A Compounded Attribute of an Entity that maps to Compounded Attribute of another Entity (another relationship?).
		-- Type of: An entity that is a sub-class or inherited from another Entity. Like a view, interface, or inheriting class type.
		-- Compounded: An attribute that is composed of multiple other attributes. Not specific to an entity.
	-- TODO: Add System Version later once the schema is locked down
	[SysStart]                DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Relationship_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]                  DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Relationship_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Relationship] PRIMARY KEY CLUSTERED ([RelationshipId] ASC),
	CONSTRAINT [FK_Relationship_Entity] FOREIGN KEY ([EntityId]) REFERENCES [AppModel].[Entity] ([EntityId]),
)
