CREATE TABLE [AppScript].[TemplateAttributeOwner]
(	-- Allows for M:N between Nodes and Attributes.
	-- An Attribute is Owned by an XML Element. Only Elements can have attributes.
	-- Allows the same attribute to be assocted with multiple Nodes.
	[TemplateId]		UniqueIdentifier NOT NULL,
	[AttributeId]		UniqueIdentifier NOT NULL,
	[NodeId]			UniqueIdentifier NOT NULL, -- Must be an Element Node. Cannot enforce this directly.
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateNodeAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateNodeAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_TemplateNodeAttribute] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [NodeId] ASC, [AttributeId] ASC),
	CONSTRAINT [FK_TemplateNode] FOREIGN KEY ([TemplateId], [NodeId]) REFERENCES [AppScript].[TemplateNode] ([TemplateId], [NodeId]),
	CONSTRAINT [FK_TemplateAttribute] FOREIGN KEY ([TemplateId], [AttributeId]) REFERENCES [AppScript].[TemplateAttribute] ([TemplateId], [AttributeId]),
)
