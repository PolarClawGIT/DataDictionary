CREATE TABLE [ProofOfConcept].[TemplateNodeOwner]
(	-- Describes the Owner of a Node within a Schema.
	-- This covers the M:N of relationships of Parent Nodes.
	[NodeId]				UniqueIdentifier Not Null, -- Child
	[NodeOwnerId]			UniqueIdentifier Not Null, -- Parent
	[SchemaId]				UniqueIdentifier Not Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateNodeOwner_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateNodeOwner_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_TemplateNodeOwner] PRIMARY KEY CLUSTERED ([NodeId] ASC, [NodeOwnerId] ASC),
	CONSTRAINT [FK_TemplateNodeOwnerChildNode] FOREIGN KEY ([SchemaId], [NodeId]) REFERENCES [ProofOfConcept].[TemplateNode] ([SchemaId], [NodeId]),
	CONSTRAINT [FK_TemplateNodeOwnerParentNode] FOREIGN KEY ([SchemaId], [NodeOwnerId]) REFERENCES [ProofOfConcept].[TemplateNode] ([SchemaId], [NodeId]),
)
