CREATE VIEW [AppCatalog].[SchemaHs] As
-- Temporal View
-- View does not enforce Alternate Keys, just returns them.
Select	F.[CatalogId], -- AK
		D.[SchemaId], -- PK
		F.[DatabaseName], -- AK
		D.[SchemaName], -- AK
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
From	[AppCatalog].[Schema] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[HsCatalog].[Schema]
			Where	[SchemaId] = D.[SchemaId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[HsCatalog].[Schema]
			Where	[SchemaId] = D.[SchemaId] And
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
			Order By [SysStart] Desc) F
GO