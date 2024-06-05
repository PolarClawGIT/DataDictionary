CREATE TABLE [App_DataDictionary].[ScriptingElement]
(
	[ElementId]             UniqueIdentifier NOT NULL CONSTRAINT [DF_ScriptingElementId] DEFAULT (newid()),
	[TemplateId]            UniqueIdentifier NOT NULL,
	-- Match To
	[PropertyScope]         [App_DataDictionary].[typeScopeName] Not Null, -- Application Scope to match to
	[PropertyName]          [App_DataDictionary].[typeNameSpaceMember] Not Null, -- Name Property to match too
	-- Element or Attribute rendering
	[AsElement]             Bit Null, -- True Script as Element. False Script as Attribute, Null  N/A do not Script
	-- XSD definition, example <xsd:element name="City" type="xsd:string">
	[ElementName]           [App_DataDictionary].[typeNameSpaceMember] Null, -- Name of the data. Used as Attribute or Element name. If Null, Column Name is used.
	[ElementType]           NVarChar(50) Null, -- The Data Type, null = do not generate. 
	[DataAs]                NVarChar(10) Not Null, -- Format of the Data coming out. Text, CData, XML
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ScriptingElement_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ScriptingElement_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ScriptingElement_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ScriptingElement] PRIMARY KEY CLUSTERED ([ElementId] ASC),
	CONSTRAINT [FK_ScriptingElementTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [App_DataDictionary].[ScriptingTemplate] ([TemplateId]),
	CONSTRAINT [CK_ScriptingElementDataAs] CHECK ([DataAs] In ('Text','CData','XML'))
)
GO
-- Nulls are possible for references. 
CREATE UNIQUE INDEX [UK_ScriptingElement_Column]
    ON [App_DataDictionary].[ScriptingElement] ([TemplateId] ASC, [PropertyScope] ASC, [PropertyName] ASC);
GO
