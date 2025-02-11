CREATE VIEW [AppCatalog].[RoutineParameterHs] AS
-- Temporal View
With [Dates] As (
	Select	[RoutineParameterId],
			[SysStart],
			[SysEnd]
	From	[AppCatalog].[RoutineParameter]
	Union
	Select	[RoutineParameterId],
			[SysStart],
			[SysEnd]
	From	[HsCatalog].[RoutineParameter]
	Where	[SysStart] != [SysEnd])
Select	FC.[CatalogId], -- AK
		FS.[SchemaId],
		FR.[RoutineId],
		D.[RoutineParameterId], -- PK
		FC.[DatabaseName], -- AK
		FS.[SchemaName], -- AK
		FR.[RoutineName], -- AK
		FR.[RoutineType],
		D.[ParameterName], -- AK
		D.[OrdinalPosition],
		D.[DataType],
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
		-- Temporal Status
		D.[SysStart], -- AK, PK
		D.[SysEnd],
		C.[ModifiedOn] As [CreatedOn],
		C.[ModifiedBy] As [CreatedBy],
		R.[ModifiedOn] As [RemovedOn],
		R.[ModifiedBy] As [RemovedBy],
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] != D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] is Null And D.[SysEnd] < SysUtcDateTime(), 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[AppCatalog].[RoutineParameter] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[RoutineParameterId] = D.[RoutineParameterId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[RoutineParameterId] = D.[RoutineParameterId] And
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
					[RoutineId],
					[SchemaId],
					[RoutineName],
					[RoutineType]
			From	[AppCatalog].[Routine]
			Where	[RoutineId] = D.[RoutineId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FR
		Outer Apply (
			Select	Top 1
					[CatalogId],
					[SchemaId],
					[SchemaName]
			From	[AppCatalog].[Schema]
			Where	[SchemaId] = FR.[SchemaId] And
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
