CREATE TABLE [AppModel].[RelationshipAttribute]
(	-- This handles the columns/Attributes of a Relationship.
	-- This is the Entity Attribute for the Relationship.
	-- Not all Relationship have columns.
	[RelationshipAttributeId] UniqueIdentifier Not Null CONSTRAINT [DF_RelationshipAttributeId] DEFAULT (newid()),
	[RelationshipId] UniqueIdentifier Not Null,
	[EntityName]     [AppModel].[typeQualifiedName] Null,
	[AttributeName]  [AppModel].[typeQualifiedName] Null,
	-- TODO: Add System Version later once the schema is locked down
	[SysStart]                DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_RelationshipAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]                  DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_RelationshipAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_RelationshipAttribute] PRIMARY KEY CLUSTERED ([RelationshipAttributeId] ASC),
	CONSTRAINT [FK_RelationshipAttribute_Relationship] FOREIGN KEY ([RelationshipId]) REFERENCES [AppModel].[Relationship] ([RelationshipId]),
)
