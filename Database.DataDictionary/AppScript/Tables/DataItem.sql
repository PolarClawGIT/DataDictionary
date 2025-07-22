CREATE TABLE [AppScript].[DataItem]
(
	[DataItemId]		UniqueIdentifier Not Null CONSTRAINT [DF_DataItemId] DEFAULT (newid()),
	[DataSourceId]		UniqueIdentifier Not Null,
	[DataItemMember]	[AppGeneral].[uddtNameSpaceMember] Not Null, -- Member Name of the alias. Combined to create a NameSpace.
	[ParentItemId]		UniqueIdentifier NULL,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DataItem_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DataItem_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DataItem] PRIMARY KEY CLUSTERED ([DataItemId] ASC),
	CONSTRAINT [UK_DataItem] UNIQUE ([DataSourceId] ASC, [DataItemId] ASC), -- Enforce all children belong to the same Data Source
	CONSTRAINT [FK_DataTimeSource] FOREIGN KEY ([DataSourceId]) REFERENCES [AppScript].[DataSource] ([DataSourceId]),
	CONSTRAINT [FK_DataItemParent] FOREIGN KEY ([DataSourceId], [ParentItemId]) REFERENCES [AppScript].[DataItem] ([DataSourceId], [DataItemId]),
	CONSTRAINT [AK_DataItemMemberName] UNIQUE ([ParentItemId] ASC, [DataItemMember] ASC)
)
