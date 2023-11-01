CREATE TYPE [App_DataDictionary].[typeLibraryMember] AS TABLE
(
	[LibraryId] UniqueIdentifier Null,
	[MemberId] UniqueIdentifier Null,
	[MemberParentId] UniqueIdentifier Null,
	[AssemblyName] NVarChar(1023) Null,
	[NameSpace] NVarChar(Max) Null,
	[MemberName] [App_DataDictionary].[typeNameSpaceElement] Not Null,
	[MemberType] NVarChar(50) Null, 
	[MemberData] XML Null
)
