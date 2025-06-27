CREATE PROCEDURE [AppLibrary].[procGetLibraryMember]
		@ModelId UniqueIdentifier = Null,
		@LibraryId UniqueIdentifier = Null,
		@MemberId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
/* Description: Performs Get on LibraryMember.
*/

Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDatetime())

Select	[LibraryId],
		[MemberId],
		[MemberParentId],
		[AssemblyName],
		[MemberNameSpace],
		[MemberName],
		[MemberType],
		[MemberData],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn],
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppLibrary].[LibraryMemberHs] D -- TODO: For System_Time All D
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@LibraryId is Null Or @LibraryId = [LibraryId]) And
		(@MemberId is Null Or @MemberId = [MemberId]) And
		(@ModelId is Null Or @ModelId In (
			Select	[ModelId]
			From	[AppLibrary].[LibraryModel] -- TODO: For System_Time As of @AsOfUtcDate
			Where	D.[LibraryId] = [LibraryId]))

GO
