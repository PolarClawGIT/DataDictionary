CREATE TYPE [App_DataDictionary].[typeLibraryMember] AS TABLE
(
	[LibraryId] UniqueIdentifier Null,
	[AssemblyName] NVarChar(1023) Null,
	[MemberNameSpace] NVarChar(Max) Null,
	[MemberName] NVarChar(500) Not Null,
	[MemberType] NVarChar(100) Null, 
	[MemberData] XML Null
)
