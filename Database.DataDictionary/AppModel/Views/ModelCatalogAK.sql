CREATE VIEW [AppModel].[ModelCatalogAK] AS
-- Temporal View (Not complete)
Select	D.[ModelId], -- PK
		P.[CatalogId],
		P.[DatabaseName], --AK
		D.[ModifiedBy],
		D.[SysStart], -- AK, PK
		D.[SysEnd]
From	[App_DataDictionary].[ModelCatalog] D
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
			Order By [SysStart] Desc) P
GO