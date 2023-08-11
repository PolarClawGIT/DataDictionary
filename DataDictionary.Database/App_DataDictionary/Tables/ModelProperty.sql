
/* This is not going to work. A view is possible.
CREATE TABLE [App_DataDictionary].[ModelProperty]
(
	[ModelId]       UniqueIdentifier NOT NULL,
	[PropertyId]    UniqueIdentifier NOT NULL,
	-- Keys
	CONSTRAINT [PK_ModelProperty] PRIMARY KEY CLUSTERED ([ModelId] ASC, [PropertyId] ASC),
	CONSTRAINT [FK_ModelPropertyModel] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelPropertyProperty] FOREIGN KEY ([PropertyId]) REFERENCES [App_DataDictionary].[ApplicationProperty] ([PropertyId]),
)
*/