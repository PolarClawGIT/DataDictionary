CREATE TABLE [AppScript].[TemplateNode]
(
	[NodeId]				UniqueIdentifier NOT NULL CONSTRAINT [DF_TemplateNode] DEFAULT (newid()),
	[TemplateId]            UniqueIdentifier NOT NULL,
	-- Match To
	[PropertyScope]         [AppGeneral].[uddtScopeName] Not Null, -- Application Scope to match to
	[PropertyName]          [AppGeneral].[uddtQualifiedName] Not Null, -- Name Entity/Attribute/Process to match too
	[NodeName]				[AppGeneral].[uddtNameSpaceMember] Null, -- Name of the data. Used as Attribute or Element name.
	[NodeValueAs]			NVarChar(50) Not Null, -- How is the data to be rendered Attribute/Element Text/CData/XML.
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_TemplateNode_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateNode_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateNode_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_TemplateNode] PRIMARY KEY CLUSTERED ([NodeId] ASC),
	CONSTRAINT [FK_TemplateNodeTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [AppScript].[Template] ([TemplateId]),
	CONSTRAINT [CK_TemplateNodeDataAs] CHECK ([NodeValueAs]='Element.XML' OR [NodeValueAs]='Element.CData' OR [NodeValueAs]='Attribute.CData' OR [NodeValueAs]='Element.Text' OR [NodeValueAs]='Attribute.Text'),
)
GO
