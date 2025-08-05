CREATE TABLE [AppScript].[TemplateAttribute]
(	-- Name & Value to add as Attributes to an XML Element.
	-- This is a Super-Type based on what type of value is being rendered.
	[AttributeId]			UniqueIdentifier NOT NULL CONSTRAINT [DF_TemplateAttribute] DEFAULT (newid()),
	[TemplateId]            UniqueIdentifier NOT NULL,
	-- Script As
	[AttributeName]			NVarChar(50) Not Null, -- Name of the XML Attribute
	[RenderOrder]			Int Not Null CONSTRAINT [Df_TemplateAttributeOrder] DEFAULT (0), -- Render the values elements in this order.
	[RenderValueAs]			NVarChar(20) Not Null, -- How to render the Value
	-- Constant Value
	[FixedValue]			NVarChar(250) NULL, -- Fixed/Constant value for the node
	-- Object Property Value
	[ObjectScope]			[AppGeneral].[uddtScopeName] Null, -- Application Scope to match to. Defines the Object type.
	[ObjectProperty]		[AppGeneral].[uddtQualifiedName] Null, -- Name (object) Property within the Entity/Attribute/Process.
	-- Model Property Value
	[ModelPropertyId]		UniqueIdentifier NULL, -- Use the Model Property value of the Entity/Attribute/Process.
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_TemplateAttribute] PRIMARY KEY CLUSTERED ([AttributeId] ASC),
	CONSTRAINT [AK_TemplateAttribute] UNIQUE ([TemplateId] ASC, [AttributeId] ASC),
	CONSTRAINT [FK_TemplateAttributeProperty] FOREIGN KEY ([ModelPropertyId]) REFERENCES [AppModel].[PropertyEnumeration] ([PropertyId]),
	CONSTRAINT [FK_TemplateAttributeTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [AppScript].[Template] ([TemplateId]),
	CONSTRAINT [CK_TemplateAttributeValueAs] CHECK ([RenderValueAs]='Attribute.Text'),
)
GO
