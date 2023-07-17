CREATE TABLE [App_DataDictionary].[DomainAttribute]
(
	[AttributeId] UniqueIdentifier Not Null CONSTRAINT [DF_DomainAttribute_AttributeId] DEFAULT (newsequentialid()),
	[AttributeParentId] UniqueIdentifier Null,
	[AttributeTitle] NVarChar(100) Not Null,
	[AttributeDescription] NVarChar(Max) Null,
	[Obsolete] As (CONVERT([bit],case when [ObsoleteDate] IS NULL then (0) else (1) end)),
	-- TODO: Add System Version later once the schema is locked down
	[ObsoleteDate] DATETIME2 Null, -- Used to flag an item as a candidate for being deleted. Null = active, anything else is Obsolete.
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainAttribute_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainAttribute] PRIMARY KEY CLUSTERED ([AttributeId] ASC),
	CONSTRAINT [FK_DomainAttribute_Parent] FOREIGN KEY ([AttributeParentId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainAttribute]
    ON [App_DataDictionary].[DomainAttribute]([AttributeTitle] ASC, [AttributeParentId] ASC);
GO
