CREATE TABLE [App_DataDictionary].[DomainRelationshipAttribute]
(	-- This entire structure may belong to Entities and Attributes. Not its own thing.
	[RelationshipId]    UniqueIdentifier Not Null,
	[AttributeId]       UniqueIdentifier Not Null,
	[EntityId]          UniqueIdentifier Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy]               SysName Not Null CONSTRAINT [DF_DomainRelationshipAttribute_ModfiedBy] DEFAULT (original_login()),
	[SysStart]                DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainRelationshipAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]                  DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainRelationshipAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainRelationshipAttribute] PRIMARY KEY CLUSTERED ([RelationshipId] ASC, [AttributeId] ASC),
	CONSTRAINT [FK_DomainRelationshipAttribute_Relationship] FOREIGN KEY ([RelationshipId]) REFERENCES [App_DataDictionary].[DomainRelationship] ([RelationshipId]),
	CONSTRAINT [FK_DomainRelationshipAttribute_Attribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
	CONSTRAINT [FK_DomainRelationshipAttribute_Entity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
)
