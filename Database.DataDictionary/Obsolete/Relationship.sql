CREATE TABLE [AppModel].[Relationship]
(
	[RelationshipId]          UniqueIdentifier Not Null CONSTRAINT [DF_RelationshipId] DEFAULT (newid()),
	[RelationshipTitle]       [App_DataDictionary].[typeTitle] Not Null,
	[RelationshipDescription] [App_DataDictionary].[typeDescription] Null,
	[RelationshipName]        [AppModel].[typeQualifiedName] Null,
	[RelationshipType]        NVarChar(20) Not Null, -- Type of Relationship
		-- Primary Key: A Compounded Attribute of an Entity that identifies that Entity.
		-- Unique Key: A Compounded Attribute of an Entity that is a Unique to that Entity.
		-- Inversion Entry: A Compounded Attribute of an Entity that is used as a key.
		-- Foreign Key: A Compounded Attribute of an Entity that maps to Compounded Attribute of another Relationship.
		-- Compound: A Compounded Attribute of an Entity (other). Typically used in Views or Processes.
	[OwnerAliasId]            UniqueIdentifier Not Null, -- Owner of the Relationship. Normally an Entity.
	[RefrenceAliasId]         UniqueIdentifier Null, -- Relationship referenced, if any. (FK's and Compound, normally)
    -- Temporal History Support
	[SysStart]                DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Relationship_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]                  DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Relationship_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Relationship] PRIMARY KEY CLUSTERED ([RelationshipId] ASC),
	CONSTRAINT [FK_RelationshipEntity_Alias] FOREIGN KEY ([OwnerAliasId]) REFERENCES [AppModel].[AliasHierarchy] ([AliasId]),
	CONSTRAINT [FK_RelationshipRefrence_Alias] FOREIGN KEY ([RefrenceAliasId]) REFERENCES [AppModel].[AliasHierarchy] ([AliasId]),
) --WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[Relationship]))
GO
/*
CREATE TABLE [HsModel].[Relationship]
(
	[RelationshipId]          UniqueIdentifier Not Null,
	[RelationshipTitle]       [App_DataDictionary].[typeTitle] Not Null,
	[RelationshipDescription] [App_DataDictionary].[typeDescription] Null,
	[RelationshipName]        [AppModel].[typeQualifiedName] Null,
	[RelationshipType]        NVarChar(20) Not Null,
	[OwnerAliasId]            UniqueIdentifier Not Null,
	[RefrenceAliasId]         UniqueIdentifier Null,
	[SysStart]                DateTime2 (7) NOT NULL,
	[SysEnd]                  DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_Relationship]
    ON [HsModel].[Relationship]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Relationship]
    ON [HsModel].[Relationship]([RelationshipId] ASC)
GO
*/