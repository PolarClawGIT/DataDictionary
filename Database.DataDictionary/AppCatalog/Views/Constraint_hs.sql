CREATE VIEW [AppCatalog].[ConstraintHs] AS
-- Temporal View
Select	FC.[CatalogId],  -- AK
		FS.[SchemaId],
		D.[ConstraintId], -- PK
		D.[TableId],
		FC.[DatabaseName], -- AK
		FS.[SchemaName], -- AK
		D.[ConstraintName], -- AK
		D.[ConstraintType],
		FT.[TableName],
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
From	[AppCatalog].[Constraint]  D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[HsCatalog].[Constraint]
			Where	[ConstraintId] = D.[ConstraintId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[HsCatalog].[Constraint]
			Where	[ConstraintId] = D.[ConstraintId] And
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
					[TableId],
					[SchemaId],
					[TableName],
					[TableType]
			From	[AppCatalog].[Table]
			Where	[TableId] = D.[TableId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FT
		Outer Apply (
			Select	Top 1
					[CatalogId],
					[SchemaId],
					[SchemaName]
			From	[AppCatalog].[Schema]
			Where	[SchemaId] = D.[SchemaId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FS
		Outer Apply (
			Select	Top 1
					[CatalogId],
					[DatabaseName]
			From	[AppCatalog].[Catalog]
			Where	[CatalogId] = FS.[CatalogId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FC
GO
