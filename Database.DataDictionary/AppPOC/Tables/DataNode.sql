CREATE TABLE [AppPOC].[DataNode]
(	-- Describes a Node within the Data Definition
	[NodeId]				UniqueIdentifier Not Null CONSTRAINT [DF_DataNode] DEFAULT (newid()),
	[DataId]				UniqueIdentifier Not Null,
	[NodeName]				[AppGeneral].[uddtMember] Null, -- Name to be used XML Element. Name of the Property is used if Null.
	[NodeOrder]				Int Not Null CONSTRAINT [Df_DataNodeOrder] DEFAULT (0), -- Render the values elements in this order.
	[RenderValueAs]			NVarChar(20) Not Null, -- How to render the Value
	-- Constant Value
	[FixedValue]			NVarChar(250) NULL, -- Fixed/Constant value for the node
	-- Object Property Value
	[ObjectScope]			[AppGeneral].[uddtScopeName] Null, -- Application Scope to match to. Defines the Object type.
	[ObjectProperty]		[AppGeneral].[uddtQualifiedName] Null, -- Name (object) Property within the Entity/Attribute/Process.
	-- Model Property Value
	[ModelPropertyId]		UniqueIdentifier NULL, -- Use the Model Property value of the Entity/Attribute/Process.
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DataNode_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DataNode_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DataNode] PRIMARY KEY CLUSTERED ([NodeId] ASC),
	CONSTRAINT [AK_DataNodeID] UNIQUE ([DataId] ASC, [NodeId] ASC), -- Used for FK refrence to insure everything belong TemplateData
	CONSTRAINT [FK_DataNodeData] FOREIGN KEY ([DataId]) REFERENCES [AppPOC].[DataDefinition] ([DataId]),
	CONSTRAINT [FK_DataNodeProperty] FOREIGN KEY ([ModelPropertyId]) REFERENCES [AppModel].[PropertyEnumeration] ([PropertyId]),
    CONSTRAINT [CK_DataNodeName] CHECK ([NodeName] like '[A-Z]%' AND NOT [NodeName] like '%[^-,^_^:^.,^A-Z,^0-9]%'),
    CONSTRAINT [CK_DataNodeValueAs] CHECK ([RenderValueAs]='Element' OR [RenderValueAs]='Element.Text' OR [RenderValueAs]='Element.XML' OR [RenderValueAs]='Element.CData' OR [RenderValueAs]='Attribute' OR [RenderValueAs]='Attribute.Text'),

)
