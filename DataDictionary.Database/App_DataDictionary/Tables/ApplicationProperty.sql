CREATE TABLE [App_DataDictionary].[ApplicationProperty]
(
	-- Works as a lookup to create/define an Extended Property.
	[PropertyId]          UniqueIdentifier NOT NULL CONSTRAINT [DF_ApplicationPropertyId] DEFAULT (newid()),
	[PropertyTitle]       [App_DataDictionary].[typeTitle] Not Null, -- Title of the Property as it appears in the application. This may contain the Property Name but must be unique for each type of Extended Property it applies to.
	[PropertyDescription] [App_DataDictionary].[typeDescription] Null,
	[PropertyName]        SysName Null, -- Name for the Extended property. Most interested in: MS_Description
	[Obsolete] As (CONVERT([bit],case when [ObsoleteDate] IS NULL then (0) else (1) end)),
	-- TODO: Add System Version later once the schema is locked down
	[ObsoleteDate] DATETIME2 Null, -- Used to flag an item as a candidate for being deleted. Null = active, anything else is Obsolete.
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ApplicationPropertyModfiedBy] DEFAULT (ORIGINAL_LOGIN()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_ApplicationProperty_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_ApplicationProperty_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ApplicationProperty] PRIMARY KEY CLUSTERED ([PropertyId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ApplicationProperty]
    ON [App_DataDictionary].[ApplicationProperty]([PropertyTitle] ASC);
GO