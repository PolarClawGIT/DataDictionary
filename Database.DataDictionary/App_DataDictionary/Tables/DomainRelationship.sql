CREATE TABLE [App_DataDictionary].[DomainRelationship]
(
	[RelationshipId]          UniqueIdentifier Not Null CONSTRAINT [DF_DomainRelationshipId] DEFAULT (newid()),
	[RelationshipTitle]       [App_DataDictionary].[typeTitle] Not Null,
	[RelationshipDescription] [App_DataDictionary].[typeDescription] Null,
	[RelationshipType]        NVarChar(20) Not Null, -- Type of Relationship
		-- Primary Key: A Compounded Attribute of an Entity that identifies that Entity.
		-- Foreign Key: A Compounded Attribute of an Entity that maps to Compounded Attribute of another Entity (another relationship?).
		-- Unique Key: A Compounded Attribute of an Entity that is a Unique to that Entity
		-- Inversion Entry: A Compounded Attribute of an Entity that is used as a (none of above) key.
		-- Compounded: An attribute that is composed of multiple other attributes. Not specific to an entity.
		-- Type of: An entity that is a sub-class or inherited from another Entity. Like a view, interface, or inheriting class type.
		-- Member of: An Attribute that is a Member of an Entity. Like table/view columns or class property/fields.
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy]               SysName Not Null CONSTRAINT [DF_DomainRelationship_ModfiedBy] DEFAULT (original_login()),
	[SysStart]                DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainRelationship_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]                  DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainRelationship_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainRelationship] PRIMARY KEY CLUSTERED ([RelationshipId] ASC),
)
