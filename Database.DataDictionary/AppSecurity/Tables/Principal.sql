CREATE TABLE [AppSecurity].[Principal]
(
	[PrincipalId] UniqueIdentifier Not Null CONSTRAINT [DF_PrincipalId] DEFAULT (newsequentialid()),
	[PrincipalLogin] SysName Not Null, -- Account Name as it appears in original_login()
	[PrincipalName] [App_DataDictionary].[typeTitle] Not Null, -- Display Name
	[PrincipalAnnotation] [App_DataDictionary].[typeDescription] Null, --Additional Notes
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_Principal_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Principal_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Principal_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_Principal] PRIMARY KEY CLUSTERED ([PrincipalId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Principal]
    ON [AppSecurity].[Principal]([PrincipalLogin] ASC);
GO