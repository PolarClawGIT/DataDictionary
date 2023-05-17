CREATE TABLE [App_DataDictionary].[DomainAttribute]
(
	[AttributeId] Int Not Null,
	[AttributeParentId] Int Null,
	[AttributeTitle] NVarChar(100) Not Null,
	[AttributeText] NVarChar(4000) Null,
	-- TODO: Add System Versioning later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainAttribute_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_DomainAttribute] PRIMARY KEY CLUSTERED ([AttributeId] ASC),
	CONSTRAINT [FK_DomainAttribute_Parent] FOREIGN KEY ([AttributeParentId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),

)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainAttribute]
    ON [App_DataDictionary].[DomainAttribute]([AttributeTitle] ASC, [AttributeParentId] ASC);
GO
