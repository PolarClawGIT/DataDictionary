CREATE VIEW [AppCatalog].[SchemaAK] As
-- Temporal View
Select	D.[SchemaId], -- PK
		D.[CatalogId], -- AK
		P.[DatabaseName], -- AK
		D.[SchemaName], -- AK
		D.[ModifiedBy],
		D.[SysStart], -- AK, PK
		D.[SysEnd],
		-- Has values only if For System_Time All. Used to determine Inserted/Updated/Deleted.
		Max(D.[SysEnd]) Over (
			Partition By D.[SchemaId]
			Order By D.[SysEnd]
			Rows Between 1 Preceding and 1 Preceding) As [PriorDate],
		Min(D.[SysStart]) Over (
			Partition by D.[SchemaId]
			Order By D.[SysStart]
			Rows Between 1 Following and 1 Following) As [NextDate]
From	[AppCatalog].[Schema] D
		Outer Apply (
			Select	Top 1
					[DatabaseName]
			From	[AppCatalog].[CatalogAK]
			Where	[CatalogId] = D.[CatalogId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) P
GO