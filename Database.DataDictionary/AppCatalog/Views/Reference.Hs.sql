CREATE VIEW [AppCatalog].[ReferenceHs] AS
-- Temporal View
With [Dates] As (
	Select	[ReferenceId],
			[SysStart],
			[SysEnd]
	From	[AppCatalog].[Reference]
	Union
	Select	[ReferenceId],
			[SysStart],
			[SysEnd]
	From	[HsCatalog].[Reference]
	Where	[SysStart] != [SysEnd])
Select	FC.[CatalogId], -- AK
		D.[ReferenceId], -- PK
		FC.[DatabaseName], --AK
		D.[SchemaName], -- AK
		D.[ObjectName], -- AK
		D.[ObjectType],
		D.[ReferencedDatabaseName], --AK
		D.[ReferencedSchemaName], -- AK
		D.[ReferencedObjectName], -- AK
		D.[ReferencedColumnName], --AK
		D.[ReferencedType],
		D.[IsCallerDependent],
		D.[IsAmbiguous],
		D.[IsSelected],
		D.[IsModified],
		D.[IsSelectAll],
		D.[IsAllColumnsFound],
		D.[IsInsertAll],
		D.[IsIncomplete],
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
From	[AppCatalog].[Reference]  D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[ReferenceId] = D.[ReferenceId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[ReferenceId] = D.[ReferenceId] And
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
					[CatalogId],
					[DatabaseName]
			From	[AppCatalog].[Catalog]
			Where	[CatalogId] = D.[CatalogId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FC