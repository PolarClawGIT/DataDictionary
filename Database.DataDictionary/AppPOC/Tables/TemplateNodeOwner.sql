CREATE TABLE [AppPOC].[TemplateNodeOwner]
(	-- This covers the M:N of relationships of Parent Nodes.
	[NodeId]				UniqueIdentifier NOT NULL,
	[NodeOwnerId]			UniqueIdentifier NOT NULL,
	[DataId]				UniqueIdentifier Not Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateNodeOwner_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateNodeOwner_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_TemplateNodeOwner] PRIMARY KEY CLUSTERED ([NodeId] ASC, [NodeOwnerId] ASC),
	CONSTRAINT [FK_TemplateChildNode] FOREIGN KEY ([DataId], [NodeId]) REFERENCES [AppPOC].[TemplateNode] ([DataId], [NodeId]),
	CONSTRAINT [FK_TemplateParentNode] FOREIGN KEY ([DataId], [NodeOwnerId]) REFERENCES [AppPOC].[TemplateNode] ([DataId], [NodeId]),
	CONSTRAINT [FK_TemplateNodeOwnerData] FOREIGN KEY ([DataId]) REFERENCES [AppPOC].[TemplateData] ([DataId]),

)
