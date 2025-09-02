CREATE TABLE [AppScript].[DataObject]
(
	[DataObjectId]		UniqueIdentifier Not Null CONSTRAINT [DF_DataObjectId] DEFAULT (newid()),
	[DataSourceId]		UniqueIdentifier Not Null,
	[ObjectNameId]		UniqueIdentifier Not Null,
	[ObjectScope]		[AppGeneral].[uddtScopeName] NOT NULL,  -- The Scope for the Application to look for the Name within
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DataObject_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DataObject_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DataObject] PRIMARY KEY CLUSTERED ([DataObjectId] ASC),
	CONSTRAINT [UK_DataObject] UNIQUE ([DataSourceId] ASC, [DataObjectId] ASC), -- Enforce all children belong to the same Data Source
	CONSTRAINT [FK_DataObjectSource] FOREIGN KEY ([DataSourceId]) REFERENCES [AppScript].[DataSource] ([DataSourceId]),
	CONSTRAINT [FK_DataObjectName] FOREIGN KEY ([DataSourceId], [ObjectNameId]) REFERENCES [AppScript].[DataObjectName] ([DataSourceId], [ObjectNameId]),
	CONSTRAINT [AK_DataObjectName] UNIQUE ([DataSourceId] ASC, [ObjectNameId] ASC, [ObjectScope] ASC)
)
