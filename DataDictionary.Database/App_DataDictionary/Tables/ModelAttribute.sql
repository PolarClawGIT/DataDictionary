CREATE TABLE [App_DataDictionary].[ModelAttribute]
(
	[ModelId] UniqueIdentifier NOT NULL,
	[AttributeId] UniqueIdentifier NOT NULL,
	[AttributeParentId] UniqueIdentifier Null, -- Parent must be in the same model as the Attribute.
	-- Keys
	CONSTRAINT [PK_ModelAttribute] PRIMARY KEY CLUSTERED ([ModelId] ASC, [AttributeId] ASC),
	CONSTRAINT [FK_ModelAttributeModel] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelAttributeDomainAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
	CONSTRAINT [FK_ModelAttributeParent] FOREIGN KEY ([ModelId], [AttributeParentId]) REFERENCES [App_DataDictionary].[ModelAttribute] ([ModelId], [AttributeId]),
)
