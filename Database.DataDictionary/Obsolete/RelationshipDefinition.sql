CREATE TABLE [AppModel].[RelationshipDefinition]
(
	[RelationshipId]    UniqueIdentifier NOT Null,
	[DefinitionId]      UniqueIdentifier NOT NULL,
	[DefinitionSummary] [App_DataDictionary].[typeDescription] Null, -- Plain Text summary, used where RTF cannot be used.
	[DefinitionText]    [AppModel].[typeRichText] Null, -- Contains Rich Text Definition. Rich Text must be handled differently.
    -- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_RelationshipDefinition_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_RelationshipDefinition_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_RelationshipDefinition] PRIMARY KEY CLUSTERED ([RelationshipId] ASC, [DefinitionId] ASC),
	CONSTRAINT [FK_RelationshipDefinitionRelationship] FOREIGN KEY ([RelationshipId]) REFERENCES [AppModel].[Relationship] ([RelationshipId]),
	CONSTRAINT [FK_RelationshipDefinitionDefinition] FOREIGN KEY ([DefinitionId]) REFERENCES [AppModel].[DefinitionEnumeration] ([DefinitionId]),
) --WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[RelationshipDefinition]))
GO
/*
CREATE TABLE [HsModel].[RelationshipDefinition]
(
	[RelationshipId]    UniqueIdentifier NOT Null,
	[DefinitionId]      UniqueIdentifier NOT NULL,
	[DefinitionSummary] [App_DataDictionary].[typeDescription] Null,
	[DefinitionText]    [AppModel].[typeRichText] Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_RelationshipDefinition]
    ON [HsModel].[RelationshipDefinition]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_RelationshipDefinition]
    ON [HsModel].[RelationshipDefinition]([RelationshipId] ASC, [DefinitionId] ASC)
GO
*/
