CREATE VIEW [AppCatalog].[DomainAK] As
-- Temporal View
-- View does not enforce Alternate Keys, just returns them.
Select	F.[CatalogId], -- AK
		F.[SchemaId],
		D.[DomainId], -- PK
		F.[DatabaseName], -- AK
		F.[SchemaName], -- AK
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
		D.[CreatedBy],
		D.[SysStart], --PK, AK
		D.[SysEnd],
		-- Temporal Status
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] <> D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] <> D.[SysEnd], 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[AppCatalog].[Domain] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[HsCatalog].[Domain]
			Where	[DomainId] = D.[DomainId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[HsCatalog].[Domain]
			Where	[DomainId] = D.[DomainId] And
					[SysStart] >= D.[SysEnd]) N
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
			Order By [SysStart] Desc) F

GO
