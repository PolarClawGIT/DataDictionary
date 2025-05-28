CREATE TABLE [AppModel].[RelationshipProperty]
(
	[RelationshipId]			UniqueIdentifier Not Null,
	[PropertyId]		UniqueIdentifier NOT Null,
	[PropertyValue]		NVarChar(4000) Null, -- The Value for the Property. (Summary Text, Extended Property, Choice)
    -- Temporal History Support
	[SysStart]			DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_RelationshipProperty_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]			DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_RelationshipProperty_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_RelationshipProperty] PRIMARY KEY CLUSTERED ([RelationshipId] ASC, [PropertyId] ASC),
	CONSTRAINT [FK_RelationshipPropertyRelationship] FOREIGN KEY ([RelationshipId]) REFERENCES [AppModel].[Relationship] ([RelationshipId]),
	CONSTRAINT [FK_RelationshipPropertyApplicationProperty] FOREIGN KEY ([PropertyId]) REFERENCES [AppModel].[PropertyEnumeration] ([PropertyId]),
) --WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[RelationshipProperty]))
GO
/*
CREATE TABLE [HsModel].[RelationshipProperty]
(
	[RelationshipId]	UniqueIdentifier Not Null,
	[PropertyId]		UniqueIdentifier NOT Null,
	[PropertyValue]		NVarChar(4000) Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_RelationshipProperty]
    ON [HsModel].[RelationshipProperty]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_RelationshipProperty]
    ON [HsModel].[RelationshipProperty]([RelationshipId] ASC, [PropertyId] ASC)
GO
*/