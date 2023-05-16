CREATE TABLE [App_DataDictionary].[ExtendedProperty]
(
	[PropertyId] Int NOT Null,
	[PropertyName] SysName Not Null,
	CONSTRAINT [PK_ExtendedProperty] PRIMARY KEY CLUSTERED ([PropertyId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ExtendedProperty]
    ON [App_DataDictionary].[ExtendedProperty]([PropertyName] ASC);
GO