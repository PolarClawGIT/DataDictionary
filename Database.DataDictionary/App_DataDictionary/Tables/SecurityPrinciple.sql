CREATE TABLE [App_DataDictionary].[SecurityPrinciple]
(
	-- To be implemented later
	[SecurityPrincipleId] UniqueIdentifier Not Null CONSTRAINT [DF_SecurityPrincipleId] DEFAULT (newsequentialid()),
	[SecurityPrinciple] NVarChar(256) Not Null,
	[DisplayName] NVarChar(1000) Not Null,
	CONSTRAINT [PK_SecurityPrinciple] PRIMARY KEY CLUSTERED ([SecurityPrincipleId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_SecurityPrinciple]
    ON [App_DataDictionary].[SecurityPrinciple]([SecurityPrinciple] ASC);
GO