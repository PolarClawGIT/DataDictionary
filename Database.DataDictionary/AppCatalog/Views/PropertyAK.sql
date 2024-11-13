CREATE VIEW [AppCatalog].[PropertyAK] As
Select	F.[CatalogId],
		D.[PropertyId],
		F.[DatabaseName],
		D.[Level0Type],
		D.[Level0Name],
		D.[Level1Type],
		D.[Level1Name],
		D.[Level2Type],
		D.[Level2Name],
		D.[ObjType],
		D.[ObjName],
		D.[PropertyName],
		D.[PropertyValue],
		D.[CreatedBy],
		D.[SysStart], -- AK, PK
		D.[SysEnd],
		-- Temporal Status
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] <> D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] <> D.[SysEnd], 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[AppCatalog].[Property] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[HsCatalog].[Property]
			Where	[PropertyId] = D.[PropertyId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[HsCatalog].[Property]
			Where	[PropertyId] = D.[PropertyId] And
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
