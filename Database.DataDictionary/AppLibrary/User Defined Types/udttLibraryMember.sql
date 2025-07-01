CREATE TYPE [AppLibrary].[udttLibraryMember] AS TABLE
(
	[LibraryId]        UniqueIdentifier Null,
	[MemberId]         UniqueIdentifier Null,
	[MemberParentId]   UniqueIdentifier Null,
	[AssemblyName]     NVarChar(1023) Null,
	[MemberNameSpace]  NVarChar(Max) Null,
	[MemberName]       [AppGeneral].[uddtNameSpaceMember] Not Null,
	[MemberType]       NVarChar(10) Null, 
	[MemberData]       XML Null,
	-- Temporal Data
	[CreatedOn]             DateTime2 (7) Null,
	[CreatedBy]             NVarChar(4000) Null,
	[RemovedOn]             DateTime2 (7) Null,
	[RemovedBy]             NVarChar(4000) Null,
	[IsInserted]            Bit Null,
	[IsUpdated]             Bit Null,
	[IsDeleted]             Bit Null,
	[IsCurrent]             Bit Null
)
