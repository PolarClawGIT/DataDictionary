CREATE TABLE [App_DataDictionary].[DomainAttributeAlias]
(
	[AttributeId] Int Not Null,
	[DomainId] Int NOT NULL,
	[EntityId] Int Not Null,
	[ObjectName] SysName Null,
	[ObjectTypeId] Int Null,
	-- TODO: Add System Versioning later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainAttributeAlias_ModfiedBy] DEFAULT (ORIGINAL_LOGIN()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_DomainAttributeAlias_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_DomainAttributeAlias_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_DomainAttributeAlias] PRIMARY KEY CLUSTERED ([AttributeId] ASC, [DomainId] ASC, [EntityId] ASC),
	CONSTRAINT [FK_DomainAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
	CONSTRAINT [FK_DomainAttributeAlias_ObjectType] FOREIGN KEY ([ObjectTypeId]) REFERENCES [App_DataDictionary].[ExtendedPropertyType] ([ObjectTypeId])
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainAttributeAlias]
    ON [App_DataDictionary].[DomainAttributeAlias]([DomainId] ASC, [EntityId] ASC, [ObjectName] ASC);
GO

