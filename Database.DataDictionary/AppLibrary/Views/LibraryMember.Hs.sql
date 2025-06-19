CREATE VIEW [AppLibrary].[LibraryMemberHs] As
-- Temporal View
With [Data] As (
	Select	[MemberId],
			[LibraryId],
			[MemberName],
			[MemberType],
			[MemberData],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[MemberName])) As [MemberNameSpace],
			Convert(NVarChar(Max),
				FormatMessage('/%I64d/', -- Under documented BigInt. See C++ PrintF
					Dense_Rank() Over (Order By [MemberName])))
				As [HierarchyId],
			[SysStart],
			[SysEnd]
	From	[AppLibrary].[LibraryMember]
	Where	[MemberParentId] is Null
	Union All
	Select	H.[MemberId],
			H.[LibraryId],
			H.[MemberName],
			H.[MemberType],
			H.[MemberData],
			Convert(NVarChar(Max),
				FormatMessage('%s.[%s]',D.[MemberNameSpace], H.[MemberName])) As [MemberNameSpace],
			Convert(NVarChar(Max), FormatMessage('%s%I64d/', D.[HierarchyId],
				Row_Number() Over (Partition By D.[MemberId] Order By H.[MemberName])))
				As [HierarchyId],
			Greatest(D.[SysStart], H.[SysStart]) As [SysStart],
			Least(D.[SysEnd], H.[SysEnd]) As [SysEnd]
	From	[Data] D
			Inner Join [AppLibrary].[LibraryMember] H
			On	D.[MemberId] = H.[MemberParentId] And
			-- Temporal, multiple rows could be returned. Do not have confidence in this.
			((D.[SysStart] >= H.[SysStart] And D.[SysStart] < H.[SysEnd]) Or
			 (H.[SysStart] >= D.[SysStart] And H.[SysStart] < D.[SysEnd]))),
[Dates] As (
	Select	[MemberId],
			[SysStart],
			[SysEnd]
	From	[AppLibrary].[LibraryMember]
	/*Union -- TODO: Temporal not yet implemented
	Select	[MemberId],
			[SysStart],
			[SysEnd]
	From	[HsLibrary].[LibraryMember]
	Where	[SysStart] != [SysEnd]*/)
Select	D.[MemberId],
		D.[MemberName],
		D.[LibraryId],
		FL.[LibraryTitle],
		D.[MemberType],
		D.[MemberData],
		D.[MemberNameSpace], --AK
		D.[HierarchyId], -- Values is not guaranteed between executions.
		-- Temporal Status
		D.[SysStart], -- AK, PK
		D.[SysEnd],
		IsNull(C.[ModifiedOn], D.[SysStart]) As [CreatedOn],
		C.[ModifiedBy] As [CreatedBy],
		IsNull(R.[ModifiedOn], NullIf(D.[SysEnd],'9999-12-31 23:59:59.9999999')) As [RemovedOn],
		R.[ModifiedBy] As [RemovedBy],
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] != D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] is Null And D.[SysEnd] < SysUtcDateTime(), 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[Data] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[MemberId] = D.[MemberId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[MemberId] = D.[MemberId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
		-- Not specifying a For System_Time returns the current value
		-- For System_Time <some date> returns the value for that date
		-- Otherwise the last value is returned
		Outer Apply (
			Select	Top 1
					[LibraryId],
					[LibraryTitle]
			From	[AppLibrary].[LibrarySource]
			Where	[LibraryId] = D.[LibraryId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FL
GO