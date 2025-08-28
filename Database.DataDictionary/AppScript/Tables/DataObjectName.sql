CREATE TABLE [AppScript].[DataObjectName]
(
	[DataNameId]		UniqueIdentifier Not Null CONSTRAINT [DF_DataNameId] DEFAULT (newid()),
	[DataSourceId]		UniqueIdentifier Not Null,
	[DataMember]		[AppGeneral].[uddtNameSpaceMember] Not Null, -- Member Name of the alias. Combined to create a NameSpace.
	[ParentNameId]		UniqueIdentifier NULL,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DataName_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DataName_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DataName] PRIMARY KEY CLUSTERED ([DataNameId] ASC),
	CONSTRAINT [UK_DataName] UNIQUE ([DataSourceId] ASC, [DataNameId] ASC), -- Enforce all children belong to the same Data Source
	CONSTRAINT [FK_DataNameSource] FOREIGN KEY ([DataSourceId]) REFERENCES [AppScript].[DataSource] ([DataSourceId]),
	CONSTRAINT [FK_DataNameParent] FOREIGN KEY ([DataSourceId], [ParentNameId]) REFERENCES [AppScript].[DataObjectName] ([DataSourceId], [DataNameId]),
	CONSTRAINT [AK_DataNameMemberName] UNIQUE ([ParentNameId] ASC, [DataMember] ASC)

)
