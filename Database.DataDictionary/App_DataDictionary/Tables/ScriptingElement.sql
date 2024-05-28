CREATE TABLE [App_DataDictionary].[ScriptingElement]
(
	[ElementId]             UniqueIdentifier NOT NULL CONSTRAINT [DF_ScriptingElementId] DEFAULT (newid()),
	[TemplateId]            UniqueIdentifier NOT NULL,
	-- Match To
	[ElementScope]          [App_DataDictionary].[typeScopeName] Not Null, -- Application Scope to match to
	[ColumnName]            SysName Not Null, -- Name Column to match too
	-- Element or Attribute rendering
	[AsElement]             Bit Null, -- True Script as Element. False Script as Attribute, Null  N/A do not Script
	-- XSD definition, example <xsd:element name="City" type="xsd:string"  minOccurs="1" nillable="true">
	[ElementName]           SysName Null, -- Name of the data. Used as Attribute or Element name. If Null, Column Name is used.
	[ElementType]           NVarChar(50) Null, -- The Data Type, null = do not generate. 
	-- Data Format, False for all = do not render data. Attribute rendering adds a "Data" attribute
	[DataAsText]            Bit Not Null, -- True Script the Data Element as plain text. False do not script the text element.
	[DataAsCData]           Bit Not Null, -- True Script the Data in a CData Element. False do not script the CData element.
	[DataAsXml]             Bit Not Null, -- True the data is a XML Element already, script it as such. False do not script as XML. If the data cannot be parsed as an XML Element, it is not rendered.
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ScriptingElement_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ScriptingElement_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ScriptingElement_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ScriptingElement] PRIMARY KEY CLUSTERED ([ElementId] ASC),
	CONSTRAINT [FK_ScriptingElementTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [App_DataDictionary].[ScriptingTemplate] ([TemplateId]),

)
