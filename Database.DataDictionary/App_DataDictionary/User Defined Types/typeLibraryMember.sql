CREATE TYPE [App_DataDictionary].[typeLibraryMember] AS TABLE
(
	[LibraryId]        UniqueIdentifier Null,
	[MemberId]         UniqueIdentifier Null,
	[MemberParentId]   UniqueIdentifier Null,
	[AssemblyName]     NVarChar(1023) Null,
	[MemberNameSpace]  NVarChar(Max) Null,
	[MemberName]       [App_DataDictionary].[typeNameSpaceMember] Not Null,
	[MemberType]       NVarChar(10) Null, 
	[MemberData]       XML Null
)
