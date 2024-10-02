CREATE TABLE [AppGeneral].[HelpSubject]
(
	[HelpId] UniqueIdentifier Not Null CONSTRAINT [DF_HelpSubjectId] DEFAULT (newid()),
	[HelpSubject] NVarChar(100) Not Null,
	[HelpToolTip] NVarChar(500) Null,
	[HelpText] NVarChar(Max) Not Null,
	[NameSpace] NVarChar(1023) Null, -- Length is based on the fact I don't use long names. 1023 is the VB.Net NameSpace definition.
	-- Temporal History Support
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_HelpSubject_ModifiedBy] DEFAULT (ORIGINAL_LOGIN()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_HelpSubject_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_HelpSubject_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_HelpDocument] PRIMARY KEY CLUSTERED ([HelpId] ASC)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsGeneral].[HelpSubject]))
GO

