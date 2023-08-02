CREATE TABLE [App_DataDictionary].[DomainAttributeProperty]
(
	[AttributeId]   UniqueIdentifier Not Null,
	[PropertyId]    UniqueIdentifier NOT Null,
	[PropertyValue] NVarChar(4000) Not Null, -- Only supporting SQLVarent as character data
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy]     SysName Not Null CONSTRAINT [DF_DomainAttributeProperty_ModfiedBy] DEFAULT (original_login()),
	[SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAttributeProperty_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAttributeProperty_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainAttributeProperty] PRIMARY KEY CLUSTERED ([AttributeId] ASC, [PropertyId] ASC),
	CONSTRAINT [FK_DomainAttributePropertyDomainAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
	CONSTRAINT [FK_DomainAttributePropertyApplicationProperty] FOREIGN KEY ([PropertyId]) REFERENCES [App_DataDictionary].[ApplicationProperty] ([PropertyId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainAttributeProperty]
    ON [App_DataDictionary].[DomainAttributeProperty]([AttributeId] ASC, [PropertyId] ASC);
GO
