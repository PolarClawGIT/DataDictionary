CREATE TABLE [App_Help].[HelpDocument]
(
	[HelpDocumentId] Int Not Null,
	[HelpSubject] NVarChar(250) Not Null,
	[HelpText] NVarChar(Max) Not Null,
	[NameSpace] NVarChar(1000) Not Null,
	-- TODO: Add System Versioning later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_HelpDocument_ModfiedBy] DEFAULT (ORIGINAL_LOGIN()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_HelpDocument_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_HelpDocument_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_HelpDocument] PRIMARY KEY CLUSTERED ([HelpDocumentId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_HelpDocument]
    ON [App_Help].[HelpDocument]([NameSpace] ASC);
GO