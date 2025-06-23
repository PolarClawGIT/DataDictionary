CREATE TYPE [AppLibrary].[udttLibraryMember] AS TABLE
(
	[LibraryId]        UniqueIdentifier Null,
	[MemberId]         UniqueIdentifier Null,
	[MemberParentId]   UniqueIdentifier Null,
	[AssemblyName]     NVarChar(1023) Null,
	[MemberNameSpace]  NVarChar(Max) Null,
	[MemberName]       [AppGeneral].[uddtNameSpaceMember] Not Null,
	[MemberType]       NVarChar(10) Null, 
	[MemberData]       XML Null
)
