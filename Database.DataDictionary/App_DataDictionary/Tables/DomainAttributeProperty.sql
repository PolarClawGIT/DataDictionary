CREATE TABLE [App_DataDictionary].[DomainAttributeProperty]
(
	[AttributeId]		UniqueIdentifier Not Null,
	[PropertyId]		UniqueIdentifier NOT Null,
	[PropertyValue]		NVarChar(4000) Null, -- The Value for the Property. (Summary Text, Extended Property, Choice)
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy]			SysName Not Null CONSTRAINT [DF_DomainAttributeProperty_ModifiedBy] DEFAULT (original_login()),
	[SysStart]			DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAttributeProperty_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]			DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAttributeProperty_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainAttributeProperty] PRIMARY KEY CLUSTERED ([AttributeId] ASC, [PropertyId] ASC),
	CONSTRAINT [FK_DomainAttributePropertyDomainAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
	CONSTRAINT [FK_DomainAttributePropertyApplicationProperty] FOREIGN KEY ([PropertyId]) REFERENCES [App_DataDictionary].[DomainProperty] ([PropertyId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainAttributeProperty]
    ON [App_DataDictionary].[DomainAttributeProperty]([AttributeId] ASC, [PropertyId] ASC);
GO
