CREATE TABLE [AppScript].[SchemaNodeOwner]
(	-- Describes the Owner of a Node within a Schema.
	-- This covers the M:N of relationships of Parent Nodes.
	[NodeId]				UniqueIdentifier Not Null, -- Child
	[NodeOwnerId]			UniqueIdentifier Not Null, -- Parent
	[SchemaId]				UniqueIdentifier Not Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_SchemaNodeOwner_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_SchemaNodeOwner_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_SchemaNodeOwner] PRIMARY KEY CLUSTERED ([NodeId] ASC, [NodeOwnerId] ASC),
	CONSTRAINT [FK_SchemaNodeOwnerChildNode] FOREIGN KEY ([SchemaId], [NodeId]) REFERENCES [AppScript].[SchemaNode] ([SchemaId], [NodeId]),
	CONSTRAINT [FK_SchemaNodeOwnerParentNode] FOREIGN KEY ([SchemaId], [NodeOwnerId]) REFERENCES [AppScript].[SchemaNode] ([SchemaId], [NodeId]),
)	WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsScript].[SchemaNodeOwner]))
