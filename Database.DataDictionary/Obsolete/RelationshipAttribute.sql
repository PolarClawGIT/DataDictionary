CREATE TABLE [AppModel].[RelationshipAttribute]
(	-- This handles the columns/Attributes of a Relationship.
	-- This is the Attribute of the Owner for the Relationship.
	[RelationshipId]          UniqueIdentifier Not Null,
	[AttributeAliasId]        UniqueIdentifier Not Null, -- Attribute within the Owner 
	[AttributeKnownAs]	      [App_DataDictionary].[typeTitle] Not Null, -- What to call the Attribute within this Relationship (default is the Attribute Name)
	[OrdinalPosition]         Int Not Null,
    -- Temporal History Support
	[SysStart]                DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_RelationshipAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]                  DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_RelationshipAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_RelationshipAttribute] PRIMARY KEY CLUSTERED ([RelationshipId], [AttributeAliasId] ASC),
	CONSTRAINT [FK_RelationshipAttribute_Relationship] FOREIGN KEY ([RelationshipId]) REFERENCES [AppModel].[Relationship] ([RelationshipId]),
	CONSTRAINT [FK_RelationshipAttribute_Alias] FOREIGN KEY ([AttributeAliasId]) REFERENCES [AppModel].[AliasHierarchy] ([AliasId]),
	CONSTRAINT [AK_RelationshipAttributeTitle] UNIQUE ([RelationshipId] ASC, [AttributeKnownAs] ASC),
	CONSTRAINT [AK_RelationshipAttributePosition] UNIQUE ([RelationshipId] ASC, [OrdinalPosition] ASC),
) --WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[RelationshipAttribute]))
GO
/*
CREATE TABLE [HsModel].[RelationshipAttribute]
(	[RelationshipId]    UniqueIdentifier Not Null,
	[AttributeAliasId]  UniqueIdentifier Not Null,
	[AttributeKnownAs]	[App_DataDictionary].[typeTitle] Not Null,
	[OrdinalPosition]   Int Not Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_RelationshipAttribute]
    ON [HsModel].[RelationshipAttribute]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_RelationshipAttribute]
    ON [HsModel].[RelationshipAttribute]([RelationshipId] ASC, [AttributeAliasId] ASC)
GO
*/