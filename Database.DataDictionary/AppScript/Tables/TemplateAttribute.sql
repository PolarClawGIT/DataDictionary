CREATE TABLE [AppScript].[TemplateAttribute]
(	-- Key/Value pairs to add as Attributes to an XML Node.
	[AttributeId]			UniqueIdentifier NOT NULL CONSTRAINT [DF_TemplateAttribute] DEFAULT (newid()),
	[NodeId]	            UniqueIdentifier NOT NULL, -- ID of the Template Node this attribute is to be assigned to
	[AttributeName]			NVarChar(50) NOT NULL, -- Name of the XML Attribute
	[AttributeValue]		NVarChar(250) NULL, -- Value of the XML Attribute (Null = use Property Value)
	[PropertyId]			UniqueIdentifier NULL, -- Use the Property Value for the XML Attribute value, if it exists.
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_TemplateAttribute_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_TemplateAttribute] PRIMARY KEY CLUSTERED ([AttributeId] ASC),
	CONSTRAINT [FK_TemplateAttribute] FOREIGN KEY ([NodeId]) REFERENCES [AppScript].[TemplateNode] ([NodeId]),
	CONSTRAINT [FK_TemplateAttributeProperty] FOREIGN KEY ([PropertyId]) REFERENCES [AppModel].[PropertyEnumeration] ([PropertyId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_TemplateAttribute]
    ON [AppScript].[TemplateAttribute]([NodeId], [AttributeName] ASC);
GO