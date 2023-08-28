CREATE TABLE [App_DataDictionary].[DomainAttribute]
(
	-- In ER diagram tools, a Domain is associated with a Data Type.
	-- An ER diagram attribute is is associated with a Column or Parameter.
	-- For this tool, the focus is on the Attribute not the Domain or the Entity.
	[AttributeId] UniqueIdentifier Not Null CONSTRAINT [DF_DomainAttributeAttributeId] DEFAULT (newid()),
	[AttributeTitle] [App_DataDictionary].[typeTitle] Not Null,
	[AttributeDescription] [App_DataDictionary].[typeDescription] Null,
	[Obsolete] As (CONVERT([bit],case when [ObsoleteDate] IS NULL then (0) else (1) end)),
	-- TODO: Add System Version later once the schema is locked down
	[ObsoleteDate] DATETIME2 Null, -- Used to flag an item as a candidate for being deleted. Null = active, anything else is Obsolete.
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainAttribute_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainAttribute] PRIMARY KEY CLUSTERED ([AttributeId] ASC),	
)
GO

