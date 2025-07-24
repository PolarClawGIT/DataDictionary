CREATE VIEW [AppScript].[DataItemHs]As
-- Temporal View
With [Data] As (
	Select	[DataSourceId],
			[DataItemId],
			[DataItemMember],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[DataItemMember])) As [DataPath],
			Convert(NVarChar(Max),
				FormatMessage('/%I64d/', -- Under documented BigInt. See C++ PrintF
					Dense_Rank() Over (Order By [DataItemMember])))
				As [HierarchyId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[DataItem]
	Where	[ParentItemId] is Null
	Union All
	Select	H.[DataSourceId],
			H.[DataItemId],
			H.[DataItemMember],
			Convert(NVarChar(Max),
				FormatMessage('%s.[%s]',D.[DataPath], H.[DataItemMember])) As [DataPath],
			Convert(NVarChar(Max), FormatMessage('%s%I64d/', D.[HierarchyId],
				Row_Number() Over (Partition By D.[DataItemId] Order By H.[DataItemMember])))
				As [HierarchyId],
			Greatest(D.[SysStart], H.[SysStart]) As [SysStart],
			Least(D.[SysEnd], H.[SysEnd]) As [SysEnd]
	From	[Data] D
			Inner Join [AppScript].[DataItem] H
			On	D.[DataItemId] = H.[ParentItemId] And
			-- Temporal, multiple rows could be returned. Do not have confidence in this.
			((D.[SysStart] >= H.[SysStart] And D.[SysStart] < H.[SysEnd]) Or
			 (H.[SysStart] >= D.[SysStart] And H.[SysStart] < D.[SysEnd]))),
[Dates] As (
	Select	[DataSourceId],
			[DataItemId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[DataItem]
	/*Union -- Temporal, not yet implemented
	Select	[DataSourceId],
			[DataItemId],
			[SysStart],
			[SysEnd]
	From	]HsScript].[DataItem]
	Where	[SysStart] != [SysEnd]*/)
Select	D.[DataSourceId], -- PK
		D.[DataItemId], -- PK
		D.[DataItemMember],
		D.[DataPath], --AK
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
			Where	[DataItemId] = D.[DataItemId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[DataItemId] = D.[DataItemId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO
