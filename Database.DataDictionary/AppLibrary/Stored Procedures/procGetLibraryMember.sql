CREATE PROCEDURE [AppLibrary].[procGetLibraryMember]
		@ModelId UniqueIdentifier = Null,
		@LibraryId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
/* Description: Performs Get on LibraryMember.
*/
Select	M.[LibraryId],
		M.[MemberId],
		M.[MemberParentId],
		L.[AssemblyName],
		N.[MemberNameSpace],
		M.[MemberName],
		M.[MemberType],
		M.[MemberData],
		-- Temporal Data
		--T.[SysStart],
		--Least(T.[SysEnd], D.[SysEnd]) As [SysEnd],
		D.[CreatedOn],
		D.[CreatedBy],
		D.[RemovedOn],
		D.[RemovedBy],
		Convert(Bit, IIF(D.[IsInserted] = 1 And T.[SysStart] = D.[SysStart], 1,0)) As [IsInserted],
		Convert(Bit, IIF(D.[IsUpdated] = 1 Or T.[SysStart] <> D.[SysStart], 1,0)) As [IsUpdated],
		Convert(Bit, IIF(D.[IsDeleted] = 1 And T.[SysStart] = D.[SysStart], 1,0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= T.[SysStart] And SysUtcDateTime() < Least(T.[SysEnd], D.[SysEnd]), 1, 0)) As [IsCurrent]

From	[AppLibrary].[LibraryMember] M
		Inner Join [AppLibrary].[LibrarySource] L
		On	M.[LibraryId] = L.[LibraryId]
		Left Join [AppLibrary].[LibraryModel] A
		On	M.[LibraryId] = A.[LibraryId]
		Cross Apply [AppLibrary].[funcGetMemberName] (M.[MemberId]) N
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@LibraryId is Null or @LibraryId = M.[LibraryId])
Order By [MemberNameSpace]
GO
