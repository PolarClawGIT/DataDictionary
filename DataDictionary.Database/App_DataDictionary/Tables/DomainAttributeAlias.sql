CREATE TABLE [App_DataDictionary].[DomainAttributeAlias]
(
	[DomainAttributeId] Int Not Null,
	[DomainAttributeAliasId] Int Not Null,
	[DatabaseName] SysName Null,
	[SchemaName] SysName Null,
	[ObjectName] SysName Null,
	[PropertyName] SysName Null,
	[ModfiedBy] SysName Not Null CONSTRAINT [Df_DomainAttributeAlias_ModfiedBy] DEFAULT (ORIGINAL_LOGIN()),
	-- TODO: Add System Versioning later once the schema is locked down
	CONSTRAINT [PK_DomainAttributeAlias] PRIMARY KEY CLUSTERED ([DomainAttributeId] ASC, [DomainAttributeAliasId] ASC),
	CONSTRAINT [FK_DomainAttribute] FOREIGN KEY ([DomainAttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([DomainAttributeId])

)
