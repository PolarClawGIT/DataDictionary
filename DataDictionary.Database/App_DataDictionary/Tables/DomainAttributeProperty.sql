CREATE TABLE [App_DataDictionary].[DomainAttributeProperty]
(
	[AttributeId] Int Not Null,
	[PropertyId] Int NOT Null,
	[PropertyValue] sql_variant Not Null,
	-- TODO: Add System Versioning later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainAttributeProperty_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_DomainAttributeProperty_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_DomainAttributeProperty_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_DomainAttributeProperty] PRIMARY KEY CLUSTERED ([AttributeId] ASC, [PropertyId] ASC),
	CONSTRAINT [FK_DomainAttributeProperty_DomainAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
)
