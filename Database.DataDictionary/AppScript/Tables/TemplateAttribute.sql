CREATE TABLE [AppScript].[TemplateAttribute]
(	-- Key/Value pairs to add as Attributes to an XML Node.
	[AttributeId]			UniqueIdentifier NOT NULL CONSTRAINT [DF_TemplateAttribute] DEFAULT (newid()),
	[TemplateId]            UniqueIdentifier NOT NULL,
	[AttributeName]			NVarChar(50) NOT NULL, -- Name of the XML Attribute
	[AttributeValue]		NVarChar(250) NULL, -- Value of the XML Attribute (Null = use Property Value)
	[PropertyId]			UniqueIdentifier NULL, -- Use the Property Value for the XML Attribute value, if it exists.
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_TemplateAttribute] PRIMARY KEY CLUSTERED ([AttributeId] ASC),
	CONSTRAINT [AK_TemplateAttribute] UNIQUE ([TemplateId] ASC, [AttributeId] ASC),
	CONSTRAINT [FK_TemplateAttributeProperty] FOREIGN KEY ([PropertyId]) REFERENCES [AppModel].[PropertyEnumeration] ([PropertyId]),
	CONSTRAINT [FK_TemplateAttributeTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [AppScript].[Template] ([TemplateId]),
)
GO
