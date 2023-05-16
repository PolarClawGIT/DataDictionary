CREATE TABLE [App_DataDictionary].[DomainAttribute]
(
	[DomainAttributeId] Int Not Null,
	[DomainAttributeParentId] Int Null,
	[DomainAttributeTitle] NVarChar(100) Not Null,
	[DomainAttributeText] NVarChar(4000) Null,
	[ModfiedBy] SysName Not Null CONSTRAINT [Df_DomainAttribute_ModfiedBy] DEFAULT (ORIGINAL_LOGIN()),
	-- TODO: Add System Versioning later once the schema is locked down
	CONSTRAINT [PK_DomainAttribute] PRIMARY KEY CLUSTERED ([DomainAttributeId] ASC),
	CONSTRAINT [FK_DomainAttribute_Parent] FOREIGN KEY ([DomainAttributeParentId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([DomainAttributeId])

)
