CREATE VIEW [AppModel].[ModelPropertyHs] AS
-- Temporal View
Select	D.[ModelId], -- PK
		FM.[ModelTitle], -- AK
		D.[PropertyId], -- PK
		FP.[PropertyTitle], -- AK
		FP.[PropertyDescription],
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
From	[AppModel].[ModelProperty] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[HsModel].[ModelProperty]
			Where	[ModelId] = D.[ModelId] And
					[PropertyId] = D.[PropertyId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[HsModel].[ModelProperty]
			Where	[ModelId] = D.[ModelId] And
					[PropertyId] = D.[PropertyId] And
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
					[PropertyId],
					[PropertyTitle],
					[PropertyDescription]
			From	[AppModel].[PropertyEnumeration]
			Where	[PropertyId] = D.[PropertyId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FP
		Outer Apply (
			Select	Top 1
					[ModelId],
					[ModelTitle]
			From	[AppModel].[Model]
			Where	[ModelId] = D.[ModelId] and
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FM
GO