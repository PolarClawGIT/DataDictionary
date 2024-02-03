CREATE TABLE [App_DataDictionary].[DomainRelationshipEntity]
(
	[RelationshipId]    UniqueIdentifier Not Null,
	[EntityId]          UniqueIdentifier Not Null,
	[Cardinality]       TinyInt Null, -- Null = infinite. Expected Null, 0 or 1. X is allowed where X is a known fixed value.
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy]               SysName Not Null CONSTRAINT [DF_DomainRelationshipEntity_ModfiedBy] DEFAULT (original_login()),
	[SysStart]                DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainRelationshipEntity_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]                  DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainRelationshipEntity_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainRelationshipEntity] PRIMARY KEY CLUSTERED ([RelationshipId] ASC, [EntityId] ASC),
	CONSTRAINT [FK_DomainRelationshipEntity_Relationship] FOREIGN KEY ([RelationshipId]) REFERENCES [App_DataDictionary].[DomainRelationship] ([RelationshipId]),
	CONSTRAINT [FK_DomainRelationshipEntity_Entity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
)
