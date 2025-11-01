CREATE TABLE [AppScript].[TemplateNode]
(	-- This is a Super-Type based on what type of value is being rendered.
	[NodeId]				UniqueIdentifier NOT NULL CONSTRAINT [DF_TemplateNode] DEFAULT (newid()),
	[TemplateId]            UniqueIdentifier NOT NULL,
	[NodeName]				[AppGeneral].[uddtMember] Not Null, -- Name to be used XML Element. Name of the Property is used if Null.
	[NodeOrder]				Int Not Null CONSTRAINT [Df_TemplateNodeOrder] DEFAULT (0), -- Render the values elements in this order.
	[RenderValueAs]			NVarChar(20) Not Null, -- How to render the Value
	-- Constant Value
	[FixedValue]			NVarChar(250) NULL, -- Fixed/Constant value for the node
	-- Object Property Value
	[ObjectScope]			[AppGeneral].[uddtScopeName] Null, -- Application Scope to match to. Defines the Object type.
	[ObjectProperty]		[AppGeneral].[uddtQualifiedName] Null, -- Name (object) Property within the Entity/Attribute/Process.
	-- Model Property Value
	[ModelPropertyId]		UniqueIdentifier NULL, -- Use the Model Property value of the Entity/Attribute/Process.
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateNode_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateNode_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_TemplateNode] PRIMARY KEY CLUSTERED ([NodeId] ASC),
	CONSTRAINT [AK_TemplateNode] UNIQUE ([TemplateId] ASC, [NodeId] ASC),
	CONSTRAINT [FK_TemplateNodeTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [AppScript].[Template] ([TemplateId]),
	CONSTRAINT [FK_TemplateNodeProperty] FOREIGN KEY ([ModelPropertyId]) REFERENCES [AppModel].[PropertyEnumeration] ([PropertyId]),
    CONSTRAINT [CK_TemplateNodeName] CHECK ([NodeName] like '[A-Z]%' AND NOT [NodeName] like '%[^-,^_^:^.,^A-Z,^0-9]%'),
    CONSTRAINT [CK_TemplateNodeValueAs] CHECK ([RenderValueAs]='Element' OR [RenderValueAs]='Element.Text' OR [RenderValueAs]='Element.XML' OR [RenderValueAs]='Element.CData' OR [RenderValueAs]='Attribute' OR [RenderValueAs]='Attribute.Text'),
)
