CREATE TYPE [App_DataDictionary].[typeDomainProperty] AS TABLE
(
	[PropertyId]          UniqueIdentifier Null,
	[PropertyTitle]       [App_DataDictionary].[typeTitle] Null,
	[PropertyDescription] [App_DataDictionary].[typeDescription] Null,
	[PropertyType]        NVarChar(20) Null,
	[PropertyData]        NVarChar(2000) Null
)
