CREATE VIEW [AppCatalog].[CatalogHs] As
-- Temporal View
-- View for Catalog is not really needed. It is here for consistency.
-- View does not enforce Alternate Keys, just returns them.
Select	D.[CatalogId], -- AK, PK
		D.[CatalogTitle],
		D.[CatalogDescription],
		D.[ServerName],
		D.[DatabaseName], -- AK
		D.[SourceDate],
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
From	[AppCatalog].[Catalog] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[HsCatalog].[Catalog]
			Where	[CatalogId] = D.[CatalogId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[HsCatalog].[Catalog]
			Where	[CatalogId] = D.[CatalogId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO
