CREATE TYPE [App_DataDictionary].[typeAlias] AS TABLE
(
	[SourceName]       NVarChar(128) Not Null,
	[AliasName]        [App_DataDictionary].[typeAliasName] Null
)
