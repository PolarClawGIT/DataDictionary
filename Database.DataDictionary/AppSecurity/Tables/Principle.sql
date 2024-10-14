CREATE TABLE [AppSecurity].[Principle]
(
	[PrincipleId] UniqueIdentifier Not Null CONSTRAINT [DF_PrincipleId] DEFAULT (newsequentialid()),
	[PrincipleLogin] SysName Not Null, -- Account Name as it appears in original_login()
	[PrincipleName] [App_DataDictionary].[typeTitle] Not Null, -- Display Name
	[PrincipleAnnotation] [App_DataDictionary].[typeDescription] Null, --Additional Notes
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_Principle_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Principle_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Principle_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_Principle] PRIMARY KEY CLUSTERED ([PrincipleId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Principle]
    ON [AppSecurity].[Principle]([PrincipleLogin] ASC);
GO