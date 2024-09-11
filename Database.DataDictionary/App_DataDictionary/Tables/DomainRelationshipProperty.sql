CREATE TABLE [App_DataDictionary].[DomainRelationshipProperty]
(
	[RelationshipId]			UniqueIdentifier Not Null,
	[PropertyId]		UniqueIdentifier NOT Null,
	[PropertyValue]		NVarChar(4000) Null, -- The Value for the Property. (Summary Text, Extended Property, Choice)
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy]			SysName Not Null CONSTRAINT [DF_DomainRelationshipProperty_ModifiedBy] DEFAULT (original_login()),
	[SysStart]			DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainRelationshipProperty_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]			DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainRelationshipProperty_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainRelationshipProperty] PRIMARY KEY CLUSTERED ([RelationshipId] ASC, [PropertyId] ASC),
	CONSTRAINT [FK_DomainRelationshipPropertyDomainRelationship] FOREIGN KEY ([RelationshipId]) REFERENCES [App_DataDictionary].[DomainRelationship] ([RelationshipId]),
	CONSTRAINT [FK_DomainRelationshipPropertyApplicationProperty] FOREIGN KEY ([PropertyId]) REFERENCES [App_DataDictionary].[DomainProperty] ([PropertyId]),
)
