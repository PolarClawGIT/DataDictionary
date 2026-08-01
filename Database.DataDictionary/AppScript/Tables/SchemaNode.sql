CREATE TABLE [AppScript].[SchemaNode]
(	-- Describes a XSD Node within the Schema Definition
	[NodeId]				UniqueIdentifier Not Null CONSTRAINT [DF_SchemaNodeId] DEFAULT (newid()),
	[SchemaId]				UniqueIdentifier Not Null,
	[ObjectScope]			[AppGeneral].[uddtScopeName] Not Null, -- Application Scope to that is the source (Entity/Attribute/Process) of the data.
	[ObjectProperty]		[AppGeneral].[uddtQualifiedName] Null, -- Name (object) Property within the within the source. Null = the Object itself.
	[NodeName]				[AppGeneral].[uddtMember] Null, -- Name to be used XML Element. Name of the Property is used if Null.
	[RenderNodeType]		NVarChar(30) Null, -- XML Type of node to Render. See: System.Xml.Schema.XmlNodeType
	[RenderTypeCode]		NVarChar(30) Null, -- XML XSD Schema Type to Render. See: System.Xml.Schema.XmlTypeCode
	[RenderOrder]			Int Not Null CONSTRAINT [Df_RenderOrder] DEFAULT (0), -- Render the values elements in this order.
	--[FixedValue]			NVarChar(250) Null, -- Fixed/Constant value for the node, as a String	
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_SchemaNode_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_SchemaNode_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_SchemaNode] PRIMARY KEY CLUSTERED ([NodeId] ASC),
	CONSTRAINT [AK_SchemaNodeID] UNIQUE ([SchemaId] ASC, [NodeId] ASC), -- Used for FK refrence to insure everything belong TemplateData
	CONSTRAINT [FK_SchemaNodeSchema] FOREIGN KEY ([SchemaId]) REFERENCES [AppScript].[SchemaDefinition] ([SchemaId]),
    CONSTRAINT [CK_SchemaNodeName] CHECK ([NodeName] like '[A-Z]%' AND NOT [NodeName] like '%[^-,^_^:^.,^A-Z,^0-9]%'),
)	WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsScript].[SchemaNode]))
Go
CREATE UNIQUE NONCLUSTERED INDEX [UX_SchemaNodeObject]
    ON [AppScript].[SchemaNode]([SchemaId], [ObjectScope], [ObjectProperty]);
GO