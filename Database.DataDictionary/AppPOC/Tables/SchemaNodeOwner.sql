CREATE TABLE [AppPOC].[SchemaNodeOwner]
(	-- Describes the Owner of a Node
	-- This covers the M:N of relationships of Parent Nodes.
	[NodeId]				UniqueIdentifier Not Null,
	[NodeOwnerId]			UniqueIdentifier Not Null,
	[DefinitionId]			UniqueIdentifier Not Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_SchemaNodeOwner_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_SchemaNodeOwner_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_SchemaNodeOwner] PRIMARY KEY CLUSTERED ([NodeId] ASC, [NodeOwnerId] ASC),
	CONSTRAINT [FK_SchemaNodeChildNode] FOREIGN KEY ([DefinitionId], [NodeId]) REFERENCES [AppPOC].[SchemaNode] ([DefinitionId], [NodeId]),
	CONSTRAINT [FK_SchemaNodeParentNode] FOREIGN KEY ([DefinitionId], [NodeOwnerId]) REFERENCES [AppPOC].[SchemaNode] ([DefinitionId], [NodeId]),
	CONSTRAINT [FK_SchemaNodeOwnerDefinition] FOREIGN KEY ([DefinitionId]) REFERENCES [AppPOC].[SchemaDefinition] ([DefinitionId]),

)
