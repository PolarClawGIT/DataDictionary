CREATE TABLE [App_DataDictionary].[ModelRelationship]
(
	[ModelId]        UniqueIdentifier NOT NULL,
	[RelationshipId] UniqueIdentifier NOT NULL,
	[SubjectAreaId]  UniqueIdentifier Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ModelRelationship_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelRelationship_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelRelationship_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelRelationship] PRIMARY KEY CLUSTERED ([ModelId] ASC, [RelationshipId] ASC),
	CONSTRAINT [FK_ModelRelationship_Model] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelRelationship_Relationship] FOREIGN KEY ([RelationshipId]) REFERENCES [App_DataDictionary].[DomainRelationship] ([RelationshipId]),
	CONSTRAINT [FK_ModelSubjectAreaRelationship] FOREIGN KEY ([ModelId], [SubjectAreaId]) REFERENCES [App_DataDictionary].[ModelSubjectArea] ([ModelId], [SubjectAreaId]),

)
