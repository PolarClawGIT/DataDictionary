CREATE VIEW [AppCatalog].[PropertyHs] AS
-- Temporal View
With [Dates] As (
	Select	[PropertyId],
			[SysStart],
			[SysEnd]
	From	[AppCatalog].[Property]
	Union
	Select	[PropertyId],
			[SysStart],
			[SysEnd]
	From	[HsCatalog].[Property]
	Where	[SysStart] != [SysEnd])
Select	FC.[CatalogId],
		D.[PropertyId],
		FC.[DatabaseName],
		D.[Level0Type],
		D.[Level0Name],
		D.[Level1Type],
		D.[Level1Name],
		D.[Level2Type],
		D.[Level2Name],
		D.[PropertyName],
		D.[PropertyValue],
		-- Temporal Status
		D.[SysStart], -- AK, PK
		D.[SysEnd],
		C.[ModifiedOn] As [CreatedOn],
		C.[ModifiedBy] As [CreatedBy],
		R.[ModifiedOn] As [RemovedOn],
		R.[ModifiedBy] As [RemovedBy],
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] != D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] is Null And D.[SysEnd] < SysUtcDateTime(), 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[AppCatalog].[Property] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[PropertyId] = D.[PropertyId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[PropertyId] = D.[PropertyId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
		Outer Apply (
			-- Not specifying a For System_Time returns the current value
			-- For System_Time <some date> returns the value for that date
			-- Otherwise the last value is returned
			Select	Top 1
					[CatalogId],
					[DatabaseName]
			From	[AppCatalog].[CatalogHs]
			Where	[CatalogId] = D.[CatalogId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FC
GO
