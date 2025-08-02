CREATE TABLE [AppScript].[DataSource]
(
	[DataSourceId]          UniqueIdentifier Not Null CONSTRAINT [DF_DataSourceId] DEFAULT (newid()),
	[DataSourceTitle]		[AppGeneral].[uddtTitle] Not Null,
	[DataSourceDescription]	[AppGeneral].[uddtDescription] Null,
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DataSource_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DataSource_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_DataSource] PRIMARY KEY CLUSTERED ([DataSourceId] ASC),

)
