CREATE TABLE [App_DataDictionary].[ApplicationModel]
(
	-- This is equivalent to a file within the file systems. Instead of saving to the file system, the data is saved to the database.
	[ModelId] UniqueIdentifier NOT NULL CONSTRAINT [DF_ApplicationModelModelId] DEFAULT (newid()),
	[ModelTitle] NVarChar(100) Not Null,
	[ModelDescription] NVarChar(1000) Null,
	[Obsolete] As (CONVERT([bit],case when [ObsoleteDate] IS NULL then (0) else (1) end)),
	-- TODO: Add System Version later once the schema is locked down
	[ObsoleteDate] DATETIME2 Null, -- Used to flag an item as a candidate for being deleted. Null = active, anything else is Obsolete.
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ApplicationModel_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ApplicationModel_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ApplicationModel_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ApplicationModel] PRIMARY KEY CLUSTERED ([ModelId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ApplicationModel]
    ON [App_DataDictionary].[ApplicationModel]([ModelTitle] ASC, [ObsoleteDate] ASC);
GO