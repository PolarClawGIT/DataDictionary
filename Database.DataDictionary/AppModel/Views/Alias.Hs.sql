CREATE VIEW [AppModel].[AliasHS] As
-- Temporal View
With [Data] As (
	Select	[AliasId],
			[AliasMember],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[AliasMember])) As [AliasNameSpace],
			Convert(NVarChar(Max),
				FormatMessage('/%I64d/', -- Under documented BigInt. See C++ PrintF
					Dense_Rank() Over (Order By [AliasMember])))
				As [HierarchyId],
			[SysStart],
			[SysEnd]
	From	[AppModel].[AliasHierarchy]
	Where	[ParentAliasId] is Null
	Union All
	Select	H.[AliasId],
			H.[AliasMember],
			Convert(NVarChar(Max),
				FormatMessage('%s.[%s]',D.[AliasNameSpace], H.[AliasMember])) As [AliasNameSpace],
			Convert(NVarChar(Max), FormatMessage('%s%I64d/', D.[HierarchyId],
				Row_Number() Over (Partition By D.[AliasId] Order By H.[AliasMember])))
				As [HierarchyId],
			Greatest(D.[SysStart], H.[SysStart]) As [SysStart],
			Least(D.[SysEnd], H.[SysEnd]) As [SysEnd]
	From	[Data] D
			Inner Join [AppModel].[AliasHierarchy] H
			On	D.[AliasId] = H.[ParentAliasId] And
			-- Temporal, multiple rows could be returned. Do not have confidence in this.
			((D.[SysStart] >= H.[SysStart] And D.[SysStart] < H.[SysEnd]) Or
			 (H.[SysStart] >= D.[SysStart] And H.[SysStart] < D.[SysEnd]))),
[Dates] As (
	Select	[AliasId],
			[SysStart],
			[SysEnd]
	From	[AppModel].[AliasHierarchy]
	Union
	Select	[AliasId],
			[SysStart],
			[SysEnd]
	From	[HsModel].[AliasHierarchy]
	Where	[SysStart] != [SysEnd])
Select	D.[AliasId], -- PK
		D.[AliasMember],
		D.[AliasNameSpace], --AK
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
			Where	[AliasId] = D.[AliasId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[AliasId] = D.[AliasId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO
