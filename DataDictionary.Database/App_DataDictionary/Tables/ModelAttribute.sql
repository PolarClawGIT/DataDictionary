CREATE TABLE [App_DataDictionary].[ModelAttribute]
(
	[ModelId] UniqueIdentifier NOT NULL,
	[AttributeId] UniqueIdentifier NOT NULL,
	-- Keys
	CONSTRAINT [PK_ModelAttribute] PRIMARY KEY CLUSTERED ([ModelId] ASC, [AttributeId] ASC),
	CONSTRAINT [FK_ModelAttributeModel] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelAttributeDomainAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
)
