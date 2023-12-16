CREATE TYPE [App_DataDictionary].[typeDomainAlias] AS TABLE
(
	[SourceName]       NVarChar(128) Not Null,
	[AliasName]        [App_DataDictionary].[typeAliasName] Null
)
