CREATE TABLE [dbo].[DomainAttributeProperty]
(
	[AttributeId] Int Not Null,
	[PropertyId] Int NOT Null,
	[PropertyValue] sql_variant Not Null,
	CONSTRAINT [PK_DomainAttributeProperty] PRIMARY KEY CLUSTERED ([AttributeId] ASC, [PropertyId] ASC),
	CONSTRAINT [FK_DomainAttributeProperty_DomainAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
)
