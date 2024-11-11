CREATE VIEW [AppCatalog].[DomainAK] As
-- Temporal View
-- View does not enforce Alternate Keys, just returns them.
Select	P.[CatalogId], -- AK
		P.[SchemaId],
		D.[DomainId], -- PK
		P.[DatabaseName], -- AK
		P.[SchemaName], -- AK
		D.[DomainName], -- AK
		D.[DataType],
		D.[DomainDefault],
		D.[CharacterMaximumLength],
		D.[CharacterOctetLength],
		D.[NumericPrecision],
		D.[NumericPrecisionRadix],
		D.[NumericScale],
		D.[DateTimePrecision],
		D.[CharacterSetCatalog],
		D.[CharacterSetSchema],
		D.[CharacterSetName],
		D.[CollationCatalog],
		D.[CollationSchema],
		D.[CollationName],
		D.[ModifiedBy],
		D.[SysStart], --PK
		D.[SysEnd],
		-- Has values only if For System_Time All. Used to determine Inserted/Updated/Deleted.
		Max(D.[SysEnd]) Over (
			Partition By D.[DomainId]
			Order By D.[SysEnd]
			Rows Between 1 Preceding and 1 Preceding) As [PriorDate],
		Min(D.[SysStart]) Over (
			Partition by D.[DomainId]
			Order By D.[SysStart]
			Rows Between 1 Following and 1 Following) As [NextDate]
From	[AppCatalog].[Domain] D
		Outer Apply (
			-- Not specifying a For System_Time returns the current value
			-- For System_Time <some date> returns the value for that date
			-- Otherwise the last value is returned
			Select	Top 1
					[CatalogId],
					[SchemaId],
					[DatabaseName],
					[SchemaName]
			From	[AppCatalog].[SchemaAK]
			Where	[SchemaId] = D.[SchemaId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) P

GO
