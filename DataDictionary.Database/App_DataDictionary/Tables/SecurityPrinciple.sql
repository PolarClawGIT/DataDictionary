CREATE TABLE [App_DataDictionary].[SecurityPrinciple]
(
	[SecurityPrincipleId] Int Not Null,
	[SecurityPrinciple] NVarChar(256) Not Null,
	[DisplayName] NVarChar(1000) Not Null,
	CONSTRAINT [PK_SecurityPrinciple] PRIMARY KEY CLUSTERED ([SecurityPrincipleId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_SecurityPrinciple]
    ON [App_DataDictionary].[SecurityPrinciple]([SecurityPrinciple] ASC);
GO