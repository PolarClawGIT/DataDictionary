CREATE VIEW [AppModel].[NameSpaceHs] As
-- Temporal View
-- This view contains a CTE over a Temporal table. This may not work as intended.
-- The view contains a HierarchyId.
--    The value is re-computed each execution and is not guaranteed to be static across insert/update/delete.
-- The view may have performance issues. 
--    If this turns out to be the case, research data-marting this view as a table and maintain with triggers.
--    Alternate, use the function instead: [AppModel].[funcGetNameSpace]
With [Data] As (
	Select	[NameSpaceId],
			[ModelId],
			[MemberName],
			Convert(NVarChar(Max), FormatMessage('[%s]', [MemberName]))
				As [NameSpace],
			Convert(NVarChar(Max),
				FormatMessage('/%I64d/', -- Under documented BigInt. See C++ PrintF
					Row_Number() Over (Order By [MemberName])))
				As [HierarchyId],
			[SysStart],
			[SysEnd]
	From	[AppModel].[NameSpaceHierarchy]
	Where	[ParentNameSpaceId] is Null
	Union All
	Select	C.[NameSpaceId],
			C.[ModelId],
			C.[MemberName],
			Convert(NVarChar(Max), FormatMessage('%s.[%s]',D.[NameSpace], C.[MemberName]))
				As [NameSpace],
			Convert(NVarChar(Max), FormatMessage('%s%I64d/', D.[HierarchyId],
				Row_Number() Over (Partition By D.[NameSpaceId] Order By C.[MemberName])))
				As [HierarchyId],
			Greatest(D.[SysStart], C.[SysStart]) As [SysStart],
			Least(D.[SysEnd], C.[SysEnd]) As [SysEnd]
	From	[Data] D
			Inner Join [AppModel].[NameSpaceHierarchy] C
			On	D.[ModelId] = C.[ModelId] And
				D.[NameSpaceId] = C.[ParentNameSpaceId] And
				-- Temporal, multiple rows could be returned. Do not have confidence in this.
				((D.[SysStart] >= C.[SysStart] And D.[SysStart] < C.[SysEnd]) Or
				 (C.[SysStart] >= D.[SysStart] And C.[SysStart] < D.[SysEnd]))
			)
Select	[NameSpaceId], -- PK
		[ModelId], -- AK
		[MemberName],
		[NameSpace], -- AK
		Convert(HierarchyId,[HierarchyId]) As [HierarchyId], -- Prototype
		-- Temporal Status
		D.[SysStart], -- AK, PK
		D.[SysEnd],
		C.[ModifiedOn] As [CreatedOn],
		C.[ModifiedBy] As [CreatedBy],
		R.[ModifiedOn] As [RemovedOn],
		R.[ModifiedBy] As [RemovedBy],
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] <> D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] <> D.[SysEnd], 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[Data] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[AppModel].[NameSpaceHierarchy]
			Where	[NameSpaceId] = D.[NameSpaceId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[AppModel].[NameSpaceHierarchy]
			Where	[NameSpaceId] = D.[NameSpaceId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO
