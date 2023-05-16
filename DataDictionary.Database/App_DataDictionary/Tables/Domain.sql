CREATE TABLE [App_DataDictionary].[Domain]
(
	[DomainId] Int NOT NULL,
	[DomainTitle] NVarChar(250) Not Null,
	[DomainDescription] NVarChar(Max) Null,
	-- TODO: Add System Versioning later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_Domain_ModfiedBy] DEFAULT (ORIGINAL_LOGIN()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_Domain_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_Domain_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_Domain] PRIMARY KEY CLUSTERED ([DomainId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Domain]
    ON [App_DataDictionary].[Domain]([DomainTitle] ASC);
GO