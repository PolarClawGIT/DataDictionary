CREATE VIEW [AppCatalog].[ConstraintColumnHs] AS
-- Temporal View 
With [Dates] As (
	Select	[ConstraintColumnId],
			[SysStart],
			[SysEnd]
	From	[AppCatalog].[ConstraintColumn]
	Union
	Select	[ConstraintColumnId],
			[SysStart],
			[SysEnd]
	From	[HsCatalog].[ConstraintColumn]
	Where	[SysStart] != [SysEnd])
Select	FC.[CatalogId], -- AK
		FS.[SchemaId],
		D.[ConstraintId],
		D.[ConstraintColumnId], -- PK
		FT.[TableId],
		D.[TableColumnId],
		FC.[DatabaseName], -- AK
		FS.[SchemaName], -- AK
		FR.[ConstraintName], -- AK
		FT.[TableName],
		FM.[ColumnName], -- AK
		D.[OrdinalPosition],
		D.[ReferencedSchemaName],
		D.[ReferencedTableName],
		D.[ReferencedColumnName],
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
From	[AppCatalog].[ConstraintColumn]  D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[ConstraintColumnId] = D.[ConstraintColumnId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[ConstraintColumnId] = D.[ConstraintColumnId] And
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
					[ConstraintName],
					[ConstraintType]
			From	[AppCatalog].[Constraint]
			Where	[ConstraintId] = D.[ConstraintId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FR
		Outer Apply (
			Select	Top 1
					[TableId],
					[SchemaId],
					[TableName],
					[TableType]
			From	[AppCatalog].[Table]
			Where	[TableId] = FR.[TableId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FT
		Outer Apply (
			Select	Top 1
					[TableId],
					[TableColumnId],
					[ColumnName]
			From	[AppCatalog].[TableColumn]
			Where	--[TableId] = FR.[TableId] And
					[TableColumnId] = D.[TableColumnId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FM
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
/*
-- Policy and Indexed Views are not compatible.
CREATE UNIQUE CLUSTERED INDEX [PK_DatabaseConstraintColumn]
    ON [AppCatalog].[DatabaseConstraintColumn_AK]([ConstraintColumnId])
GO
CREATE UNIQUE INDEX [AK_DatabaseConstraintColumn]
    ON [AppCatalog].[DatabaseConstraintColumn_AK]([DatabaseName] ASC, [SchemaName] ASC, [ConstraintName] ASC, [ColumnName] ASC, [CatalogId] ASC)
GO
*/