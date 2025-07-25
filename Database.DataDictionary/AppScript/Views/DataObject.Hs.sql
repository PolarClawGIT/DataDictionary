CREATE VIEW [AppScript].[DataObjectHs]As
-- Temporal View
With [Data] As (
	Select	[DataSourceId],
			[DataObjectId],
			[DataMember],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[DataMember])) As [DataPath],
			Convert(NVarChar(Max),
				FormatMessage('/%I64d/', -- Under documented BigInt. See C++ PrintF
					Dense_Rank() Over (Order By [DataMember])))
				As [HierarchyId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[DataObject]
	Where	[ParentObjectId] is Null
	Union All
	Select	H.[DataSourceId],
			H.[DataObjectId],
			H.[DataMember],
			Convert(NVarChar(Max),
				FormatMessage('%s.[%s]',D.[DataPath], H.[DataMember])) As [DataPath],
			Convert(NVarChar(Max), FormatMessage('%s%I64d/', D.[HierarchyId],
				Row_Number() Over (Partition By D.[DataObjectId] Order By H.[DataMember])))
				As [HierarchyId],
			Greatest(D.[SysStart], H.[SysStart]) As [SysStart],
			Least(D.[SysEnd], H.[SysEnd]) As [SysEnd]
	From	[Data] D
			Inner Join [AppScript].[DataObject] H
			On	D.[DataObjectId] = H.[ParentObjectId] And
			-- Temporal, multiple rows could be returned. Do not have confidence in this.
			((D.[SysStart] >= H.[SysStart] And D.[SysStart] < H.[SysEnd]) Or
			 (H.[SysStart] >= D.[SysStart] And H.[SysStart] < D.[SysEnd]))),
[Dates] As (
	Select	[DataSourceId],
			[DataObjectId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[DataObject]
	/*Union -- Temporal, not yet implemented
	Select	[DataSourceId],
			[DataObjectId],
			[SysStart],
			[SysEnd]
	From	]HsScript].[DataItem]
	Where	[SysStart] != [SysEnd]*/)
Select	D.[DataSourceId], -- PK
		D.[DataObjectId], -- PK
		D.[DataMember],
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
			Where	[DataObjectId] = D.[DataObjectId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[DataObjectId] = D.[DataObjectId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO
