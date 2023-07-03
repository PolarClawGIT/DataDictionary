CREATE TABLE [App_DataDictionary].[ApplicationAttribute]
(
	[ModelId] UniqueIdentifier NOT NULL,
	[AttributeId] UniqueIdentifier NOT NULL,
	-- Keys
	CONSTRAINT [PK_ApplicationAttribute] PRIMARY KEY CLUSTERED ([ModelId] ASC, [AttributeId] ASC),
	CONSTRAINT [FK_ApplicationAttribute_Model] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[ApplicationModel] ([ModelId]),
	CONSTRAINT [FK_ApplicationDomainAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
)
