CREATE VIEW [AppCatalog].[CatalogAK] As
-- Temporal View
-- View for Catalog is not really needed. It is here for consistency.
-- View does not enforce Alternate Keys, just returns them.
Select	D.[CatalogId], -- AK, PK
		D.[CatalogTitle],
		D.[CatalogDescription],
		D.[ServerName],
		D.[DatabaseName], -- AK
		D.[SourceDate],
		D.[ModifiedBy],
		D.[SysStart], -- AK, PK
		D.[SysEnd],
		-- Has values only if For System_Time All. Used to determine Inserted/Updated/Deleted.
		Max(D.[SysEnd]) Over (
			Partition By D.[CatalogId]
			Order By D.[SysEnd]
			Rows Between 1 Preceding and 1 Preceding) As [PriorDate],
		Min(D.[SysStart]) Over (
			Partition by D.[CatalogId]
			Order By D.[SysStart]
			Rows Between 1 Following and 1 Following) As [NextDate]
From	[AppCatalog].[Catalog] D
GO
