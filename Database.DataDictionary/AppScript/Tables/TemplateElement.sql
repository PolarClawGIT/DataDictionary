CREATE TABLE [AppScript].[TemplateElement]
(	-- Name & Value to definie an XML Element
	-- This is a Super-Type based on what type of value is being rendered.
	[ElementId]				UniqueIdentifier NOT NULL CONSTRAINT [DF_TemplateElement] DEFAULT (newid()),
	[TemplateId]            UniqueIdentifier NOT NULL,
	[ParentElementId]		UniqueIdentifier NULL,
	-- Render As
	[ElementName]			[AppGeneral].[uddtQualifiedName] Not Null, -- Name to be used XML Element. Name of the Property is used if Null.
	[RenderOrder]			Int Not Null CONSTRAINT [Df_TemplateElementOrder] DEFAULT (0), -- Render the values elements in this order.
	[RenderValueAs]			NVarChar(10) Not Null, -- How to render the Value
	-- Constant Value
	[FixedValue]			NVarChar(250) NULL, -- Fixed/Constant value for the node
	-- Object Property Value
	[ObjectScope]			[AppGeneral].[uddtScopeName] Null, -- Application Scope to match to. Defines the Object type.
	[ObjectProperty]		[AppGeneral].[uddtQualifiedName] Null, -- Name (object) Property within the Entity/Attribute/Process.
	-- Model Property Value
	[ModelPropertyId]		UniqueIdentifier NULL, -- Use the Model Property value of the Entity/Attribute/Process.
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateElement_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateElement_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_TemplateElement] PRIMARY KEY CLUSTERED ([ElementId] ASC),
	CONSTRAINT [AK_TemplateElement] UNIQUE ([TemplateId] ASC, [ElementId] ASC),
	CONSTRAINT [AK_TemplateElement_ElementName] UNIQUE ([ParentElementId] ASC, [ElementName] ASC),
	CONSTRAINT [FK_TemplateElementTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [AppScript].[Template] ([TemplateId]),
	CONSTRAINT [FK_TemplateElementParent] FOREIGN KEY ([TemplateId], [ParentElementId]) REFERENCES [AppScript].[TemplateElement] ([TemplateId], [ElementId]),
	CONSTRAINT [FK_TemplateElementProperty] FOREIGN KEY ([ModelPropertyId]) REFERENCES [AppModel].[PropertyEnumeration] ([PropertyId]),
	CONSTRAINT [CK_TemplateElementValueAs] CHECK ([RenderValueAs]='Text' OR [RenderValueAs]='XML' OR [RenderValueAs]='CData'),
)
