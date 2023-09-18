CREATE TABLE [App_DataDictionary].[ModelEntity]
(
	[ModelId] UniqueIdentifier NOT NULL,
	[EntityId] UniqueIdentifier NOT NULL,
	[EntityParentId] UniqueIdentifier Null,
	-- Keys
	CONSTRAINT [PK_ModelEntity] PRIMARY KEY CLUSTERED ([ModelId] ASC, [EntityId] ASC),
	CONSTRAINT [FK_ModelEntity_Model] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelDomainEntity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
	CONSTRAINT [FK_ModelEntityParent] FOREIGN KEY ([ModelId], [EntityParentId]) REFERENCES [App_DataDictionary].[ModelEntity] ([ModelId], [EntityId]),
)
