CREATE TABLE [App_DataDictionary].[ApplicationHelp]
(
	[HelpId] UniqueIdentifier Not Null CONSTRAINT [DF_ApplicationHelp_HelpId] DEFAULT (NEWSEQUENTIALID()),
	[HelpParentId] UniqueIdentifier Null,
	[HelpSubject] NVarChar(250) Not Null,
	[HelpText] NVarChar(Max) Not Null,
	[NameSpace] NVarChar(1000) Not Null,
	-- TODO: Add System Versioning later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ApplicationHelp_ModfiedBy] DEFAULT (ORIGINAL_LOGIN()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_ApplicationHelp_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_HApplicationHelp_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_HelpDocument] PRIMARY KEY CLUSTERED ([HelpId] ASC),
	CONSTRAINT [FK_ApplicationHelp_Parent] FOREIGN KEY ([HelpParentId]) REFERENCES [App_DataDictionary].[ApplicationHelp] ([HelpId]),
)
GO

