CREATE TABLE [App_DataDictionary].[DomainRelationship]
(
	[RelationshipId]          UniqueIdentifier Not Null CONSTRAINT [DF_DomainRelationshipId] DEFAULT (newid()),
	[RelationshipTitle]       [App_DataDictionary].[typeTitle] Not Null,
	[RelationshipDescription] [App_DataDictionary].[typeDescription] Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy]               SysName Not Null CONSTRAINT [DF_DomainRelationship_ModfiedBy] DEFAULT (original_login()),
	[SysStart]                DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainRelationship_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]                  DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainRelationship_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainRelationship] PRIMARY KEY CLUSTERED ([RelationshipId] ASC),
)
