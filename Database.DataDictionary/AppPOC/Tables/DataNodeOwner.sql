CREATE TABLE [AppPOC].[DataNodeOwner]
(	-- Describes the Owner of a Node
	-- This covers the M:N of relationships of Parent Nodes.
	[NodeId]				UniqueIdentifier Not Null,
	[NodeOwnerId]			UniqueIdentifier Not Null,
	[DataId]				UniqueIdentifier Not Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DataNodeOwner_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DataNodeOwner_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DataNodeOwner] PRIMARY KEY CLUSTERED ([NodeId] ASC, [NodeOwnerId] ASC),
	CONSTRAINT [FK_DataNodeChildNode] FOREIGN KEY ([DataId], [NodeId]) REFERENCES [AppPOC].[DataNode] ([DataId], [NodeId]),
	CONSTRAINT [FK_DataNodeParentNode] FOREIGN KEY ([DataId], [NodeOwnerId]) REFERENCES [AppPOC].[DataNode] ([DataId], [NodeId]),
	CONSTRAINT [FK_DataNodeOwnerData] FOREIGN KEY ([DataId]) REFERENCES [AppPOC].[DataDefinition] ([DataId]),

)
