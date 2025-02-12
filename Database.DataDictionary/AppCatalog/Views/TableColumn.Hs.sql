CREATE VIEW [AppCatalog].[TableColumnHs] AS
-- Temporal View
With [Dates] As (
	Select	[TableColumnId],
			[SysStart],
			[SysEnd]
	From	[AppCatalog].[TableColumn]
	Union
	Select	[TableColumnId],
			[SysStart],
			[SysEnd]
	From	[HsCatalog].[TableColumn]
	Where	[SysStart] != [SysEnd])
Select	FC.[CatalogId], -- AK
		FS.[SchemaId],
		FT.[TableId],
		D.[TableColumnId], -- PK
		FC.[DatabaseName], -- AK
		FS.[SchemaName], -- AK
		FT.[TableName], -- AK
		FT.[TableType],
		D.[ColumnName], -- AK
		D.[OrdinalPosition],
		D.[IsNullable],
		D.[DataType],
		D.[ColumnDefault],
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
		D.[DomainCatalog],
		D.[DomainSchema],
		D.[DomainName],
		D.[IsIdentity],
		D.[IsHidden],
		D.[IsComputed],
		D.[ComputedDefinition],
		D.[GeneratedAlwayType],
		-- Temporal Status
		D.[SysStart], -- AK, PK
		D.[SysEnd],
		IsNull(C.[ModifiedOn], D.[SysStart]) As [CreatedOn],
		C.[ModifiedBy] As [CreatedBy],
		IsNull(R.[ModifiedOn], NullIf(D.[SysEnd],'9999-12-31 23:59:59.9999999')) As [RemovedOn],
		R.[ModifiedBy] As [RemovedBy],
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] != D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] is Null And D.[SysEnd] < SysUtcDateTime(), 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[AppCatalog].[TableColumn] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[TableColumnId] = D.[TableColumnId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[TableColumnId] = D.[TableColumnId] And
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
			Where	[SchemaId] = FT.[SchemaId] And
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
