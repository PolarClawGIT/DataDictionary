CREATE TABLE [App_DataDictionary].[ScriptingNodeAttribute]
(	-- Key/Value pairs to add as Attributes to an XML Node.
	[AttributeId]			UniqueIdentifier NOT NULL CONSTRAINT [DF_ScriptingNodeAttribute] DEFAULT (newid()),
	[NodeId]	            UniqueIdentifier NOT NULL, -- ID of the Template Node this attribute is to be assigned to
	[AttributeName]			NVarChar(50) NOT NULL, -- Name of the XML Attribute
	[AttributeValue]		NVarChar(250) NULL, -- Value of the XML Attribute (Null = use Property Value)
	[PropertyId]			UniqueIdentifier NULL, -- Use the Property Value for the XML Attribute value, if it exists.
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ScriptingNodeAttribute_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ScriptingNodeAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ScriptingNodeAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ScriptingNodeAttribute] PRIMARY KEY CLUSTERED ([AttributeId] ASC),
	CONSTRAINT [FK_ScriptingNodeAttribute] FOREIGN KEY ([NodeId]) REFERENCES [App_DataDictionary].[ScriptingNode] ([NodeId]),
	CONSTRAINT [FK_ScriptingNodeAttributeProperty] FOREIGN KEY ([PropertyId]) REFERENCES [App_DataDictionary].[DomainProperty] ([PropertyId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ScriptingNodeAttribute]
    ON [App_DataDictionary].[ScriptingNodeAttribute]([NodeId], [AttributeName] ASC);
GO