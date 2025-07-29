CREATE TABLE [AppScript].[TemplateNode]
(	-- Node is a super type of XML Attribute and XML Element
	[NodeId]				UniqueIdentifier NOT NULL CONSTRAINT [DF_TemplateNode] DEFAULT (newid()),
	[TemplateId]            UniqueIdentifier NOT NULL,
	-- Match To
	[PropertyScope]         [AppGeneral].[uddtScopeName] Not Null, -- Application Scope to match to. Defines the Object type.
	[PropertyName]          [AppGeneral].[uddtQualifiedName] Not Null, -- Name (object) Property within the Entity/Attribute/Process.
	[NodeName]				[AppGeneral].[uddtQualifiedName] Null, -- Name to be used XML Node. Used as Attribute or Element name.
	[NodeValueAs]			NVarChar(50) Not Null, -- How is the data to be rendered Attribute/Element Text/CData/XML.
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateNode_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateNode_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_TemplateNode] PRIMARY KEY CLUSTERED ([NodeId] ASC),
	CONSTRAINT [AK_TemplateNode] UNIQUE ([TemplateId] ASC, [NodeId] ASC),
	CONSTRAINT [FK_TemplateNodeTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [AppScript].[Template] ([TemplateId]),
	CONSTRAINT [CK_TemplateNodeDataAs] CHECK ([NodeValueAs]='Element.XML' OR [NodeValueAs]='Element.CData' OR [NodeValueAs]='Element.Text' OR [NodeValueAs]='Attribute.Text' OR [NodeValueAs]='Attribute.CData'),
)
GO
-- Cannot enforce that only elements can have Attributes
/*
Create Unique Index [AK_TemplateNodeElement]
	On [AppScript].[TemplateNode] ([TemplateId], [NodeId])
	Where ([NodeValueAs] In ('Element.XML', 'Element.CData', 'Element.Text'))
GO
*/