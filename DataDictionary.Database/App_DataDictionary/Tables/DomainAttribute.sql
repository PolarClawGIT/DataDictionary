CREATE TABLE [App_DataDictionary].[DomainAttribute]
(
	[AttributeId] Int Not Null,
	[AttributeParentId] Int Null,
	[AttributeTitle] NVarChar(100) Not Null,
	[ObjectTypeId] Int Not Null,
	[AttributeText] NVarChar(4000) Null,
	[ModfiedBy] SysName Not Null CONSTRAINT [Df_DomainAttribute_ModfiedBy] DEFAULT (original_login()),
	-- TODO: Add System Versioning later once the schema is locked down
	CONSTRAINT [PK_DomainAttribute] PRIMARY KEY CLUSTERED ([AttributeId] ASC),
	CONSTRAINT [FK_DomainAttribute_Parent] FOREIGN KEY ([AttributeParentId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
	CONSTRAINT [FK_DomainAttribute_ObjectType] FOREIGN KEY ([ObjectTypeId]) REFERENCES [App_DataDictionary].[ExtendedPropertyType] ([ObjectTypeId])
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainAttribute]
    ON [App_DataDictionary].[DomainAttribute]([AttributeTitle] ASC, [AttributeParentId] ASC);
GO
