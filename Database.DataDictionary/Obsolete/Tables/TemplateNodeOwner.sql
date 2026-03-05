CREATE TABLE [Obsolete].[TemplateNodeOwner]
(	-- This covers the M:N of relationships of Parent Nodes.
	[NodeId]				UniqueIdentifier NOT NULL,
	[NodeOwnerId]			UniqueIdentifier NOT NULL,
	[TemplateId]            UniqueIdentifier NOT NULL,
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateParentNode_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateParentNode_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_TemplateParentNode] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [NodeId] ASC, [NodeOwnerId] ASC),
	CONSTRAINT [FK_TemplateParentNodeTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [Obsolete].[Template] ([TemplateId]),
	CONSTRAINT [FK_TemplateChildNode] FOREIGN KEY ([TemplateId], [NodeId]) REFERENCES [Obsolete].[TemplateNode] ([TemplateId], [NodeId]),
	CONSTRAINT [FK_TemplateParentNode] FOREIGN KEY ([TemplateId], [NodeOwnerId]) REFERENCES [Obsolete].[TemplateNode] ([TemplateId], [NodeId]),
)
