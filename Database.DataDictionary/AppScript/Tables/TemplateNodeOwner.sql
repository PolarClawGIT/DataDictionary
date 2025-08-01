CREATE TABLE [AppScript].[TemplateNodeOwner]
(	-- Allows for M:N between Elements and Attributes.
	-- An XML Attribute is Owned by an XML Element.
	-- Un-owned Attributes are assocated to the Root Element.
	-- Allows the same attribute to be assocted with multiple Elements.
	[TemplateId]		UniqueIdentifier NOT NULL,
	[AttributeId]		UniqueIdentifier NOT NULL,
	[ElementId]			UniqueIdentifier NOT NULL,
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateNodeAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateNodeAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_TemplateNodeAttribute] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [ElementId] ASC, [AttributeId] ASC),
	CONSTRAINT [FK_TemplateElement] FOREIGN KEY ([TemplateId], [ElementId]) REFERENCES [AppScript].[TemplateElement] ([TemplateId], [ElementId]),
	CONSTRAINT [FK_TemplateAttribute] FOREIGN KEY ([TemplateId], [AttributeId]) REFERENCES [AppScript].[TemplateAttribute] ([TemplateId], [AttributeId]),
)
