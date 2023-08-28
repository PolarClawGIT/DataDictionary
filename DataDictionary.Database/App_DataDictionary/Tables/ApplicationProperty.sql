CREATE TABLE [App_DataDictionary].[ApplicationProperty]
(
	-- Works as a lookup to create/define an Extended Property.
	[PropertyId]         UniqueIdentifier NOT NULL CONSTRAINT [DF_ApplicationPropertyPropertyId] DEFAULT (newid()),
	[PropertyTitle]      [App_DataDictionary].[typeTitle] Not Null, -- Title of the Property as it appears in the application. This may contain the Property Name but must be unique for each type of Extended Property it applies to.
	[PropertyName]       SysName Null, -- Name for the Extended property. Most interested in: MS_Description
 	-- It is not intended to delete things belonging to the model. But they can be marked Obsolete.
	[Obsolete] As (CONVERT([bit],case when [ObsoleteDate] IS NULL then (0) else (1) end)),
	[ObsoleteDate]       DATETIME2 Null, -- Used to flag an item as an Obsolete item (should not be used). Null = active, anything else is Obsolete.
	-- Keys
	CONSTRAINT [PK_ApplicationProperty] PRIMARY KEY CLUSTERED ([PropertyId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ApplicationProperty]
    ON [App_DataDictionary].[ApplicationProperty]([PropertyTitle] ASC);
GO