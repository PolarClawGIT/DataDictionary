CREATE TABLE [App_DataDictionary].[SecurityRole]
(
	-- To be implemented later
	[SecurityRoleId] Int Not Null,
	[SecurityRoleName] NVarChar(256) Not Null,
	CONSTRAINT [PK_SecurityRole] PRIMARY KEY CLUSTERED ([SecurityRoleId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_SecurityRole]
    ON [App_DataDictionary].[SecurityRole]([SecurityRoleName] ASC);
GO