﻿CREATE TABLE [App_DataDictionary].[DomainAttribute]
(
	-- In ER diagram tools, a Domain is associated with a Data Type.
	-- An ER diagram attribute is is associated with a Column or Parameter.
	-- For this tool, the focus is on the Attribute not the Domain or the Entity.
	[AttributeId]          UniqueIdentifier Not Null CONSTRAINT [DF_DomainAttributeId] DEFAULT (newid()),
	[AttributeTitle]       [App_DataDictionary].[typeTitle] Not Null,
	[AttributeDescription] [App_DataDictionary].[typeDescription] Null,
	[TypeOfAttributeId]    UniqueIdentifier Null, -- Allows for sub-types
	[IsSingleValue]        Bit Null, -- A Simple Valued attribute has a distinct value (not Multi Valued)
	[IsMultiValue]         As (convert(bit, case when [IsSingleValue]=(0) then (1) when [IsSingleValue]=(1) then (0) end)),
	[IsSimple]             Bit Null, -- A Simple attribute (not Composite)
	[IsComposite]          As (convert(bit, case when [IsSimple]=(0) then (1) when [IsSimple]=(1) then (0) end)),
    [IsDerived]            Bit Null, -- A Derived attribute is computed from other attributes(s).
	[IsNullable]           Bit Null, -- A Null-able attribute can contain a Null Value
	[IsKey]                Bit Null, -- A Key attribute can identify an Entity
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainAttribute_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainAttribute] PRIMARY KEY CLUSTERED ([AttributeId] ASC),
	CONSTRAINT [FK_DomainAttribute_TypeOfAttribute] FOREIGN KEY ([TypeOfAttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
)
GO

