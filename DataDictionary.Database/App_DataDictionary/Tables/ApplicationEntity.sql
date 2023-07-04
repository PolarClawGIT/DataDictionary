CREATE TABLE [App_DataDictionary].[ApplicationEntity]
(
	[ModelId] UniqueIdentifier NOT NULL,
	[EntityId] UniqueIdentifier NOT NULL,
	-- Keys
	CONSTRAINT [PK_ApplicationEntity] PRIMARY KEY CLUSTERED ([ModelId] ASC, [EntityId] ASC),
	CONSTRAINT [FK_ApplicationEntity_Model] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[ApplicationModel] ([ModelId]),
	CONSTRAINT [FK_ApplicationDomainEntity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
)
