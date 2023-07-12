CREATE TABLE [App_DataDictionary].[DomainAttributeProperty]
(
	[AttributeId] UniqueIdentifier Not Null,
	[PropertyName] SysName Not Null, -- It is assumed that the Property Name when combined with the Alias details is enough information to create the extended property
	[PropertyValue] NVarChar(4000) Not Null, -- Only supporting SQLVarent as character data
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainAttributeProperty_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAttributeProperty_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAttributeProperty_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainAttributeProperty] PRIMARY KEY CLUSTERED ([AttributeId] ASC, [PropertyName] ASC),
	CONSTRAINT [FK_DomainAttributeProperty_DomainAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainAttributeProperty]
    ON [App_DataDictionary].[DomainAttributeProperty]([AttributeId] ASC, [PropertyName] ASC);
GO
