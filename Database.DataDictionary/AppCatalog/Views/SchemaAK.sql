CREATE VIEW [AppCatalog].[SchemaAK] As
-- Temporal View
-- View does not enforce Alternate Keys, just returns them.
Select	F.[CatalogId], -- AK
		D.[SchemaId], -- PK
		F.[DatabaseName], -- AK
		D.[SchemaName], -- AK
		D.[CreatedBy],
		D.[SysStart], -- AK, PK
		D.[SysEnd],
		-- Temporal Status
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
		Outer Apply (
			-- Not specifying a For System_Time returns the current value
			-- For System_Time <some date> returns the value for that date
			-- Otherwise the last value is returned
			Select	Top 1
					[CatalogId],
					[DatabaseName]
			From	[AppCatalog].[CatalogAK]
			Where	[CatalogId] = D.[CatalogId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) F
GO