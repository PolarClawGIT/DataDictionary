CREATE TABLE [App_DataDictionary].[ScriptingNode]
(
	[NodeId]				UniqueIdentifier NOT NULL CONSTRAINT [DF_ScriptingNode] DEFAULT (newid()),
	[TemplateId]            UniqueIdentifier NOT NULL,
	-- Match To
	[PropertyScope]         [App_DataDictionary].[typeScopeName] Not Null, -- Application Scope to match to
	[PropertyName]          [App_DataDictionary].[typeNameSpaceMember] Not Null, -- Name Property to match too
	[NodeName]				[App_DataDictionary].[typeNameSpaceMember] Null, -- Name of the data. Used as Attribute or Element name. If Null, Column Name is used.
	[NodeValueAs]			NVarChar(50) Not Null, -- How is the data to be rendered Attribute/Element Text/CData/XML.
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_ScriptingNode_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ScriptingNode_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ScriptingNode_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ScriptingNode] PRIMARY KEY CLUSTERED ([NodeId] ASC),
	CONSTRAINT [FK_ScriptingNodeTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [App_DataDictionary].[ScriptingTemplate] ([TemplateId]),
	CONSTRAINT [CK_ScriptingNodeDataAs] CHECK ([NodeValueAs]='Element.XML' OR [NodeValueAs]='Element.CData' OR [NodeValueAs]='Attribute.CData' OR [NodeValueAs]='Element.Text' OR [NodeValueAs]='Attribute.Text'),
)
GO
-- Nulls are possible for references. 
CREATE UNIQUE INDEX [UK_ScriptingNode_Column]
    ON [App_DataDictionary].[ScriptingNode] ([TemplateId] ASC, [PropertyScope] ASC, [PropertyName] ASC);
GO
