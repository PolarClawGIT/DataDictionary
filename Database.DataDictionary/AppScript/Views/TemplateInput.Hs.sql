CREATE VIEW [AppScript].[TemplateInputHs] As
-- Temporal View
-- Template Input Data (M:N for Template and DataSource) only has meaning within a Model.
With [Dates] As (
	Select	IsNull([TemplateId], CAST(0x0 AS UNIQUEIDENTIFIER)) As [TemplateId],
			IsNull([DataSourceId], CAST(0x0 AS UNIQUEIDENTIFIER)) As [DataSourceId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[ScriptingModel]
	Where	[TemplateId] is not null And
			[DataSourceId] is not null
	/*Union -- TODO: Temporal not yet implemented
	Select	IsNull([TemplateId], CAST(0x0 AS UNIQUEIDENTIFIER)) As [TemplateId],
			IsNull([DataSourceId], CAST(0x0 AS UNIQUEIDENTIFIER)) As [DataSourceId],
			[SysStart],
			[SysEnd]
	From	[HsScript].[ScriptingModel]
	Where	[SysStart] != [SysEnd] And
			[TemplateId] is not null And
			[DataSourceId] is not null*/)
Select	D.[ModelId],
		D.[TemplateId],
		D.[DataSourceId],
		FT.[TemplateTitle],
		FD.[DataSourceTitle],
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
From	[AppScript].[ScriptingModel] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[TemplateId] = IsNull(D.[TemplateId], CAST(0x0 AS UNIQUEIDENTIFIER))  And
					[DataSourceId] = IsNull(D.[DataSourceId], CAST(0x0 AS UNIQUEIDENTIFIER)) And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[TemplateId] = IsNull(D.[TemplateId], CAST(0x0 AS UNIQUEIDENTIFIER)) And
					[DataSourceId] = IsNull(D.[DataSourceId], CAST(0x0 AS UNIQUEIDENTIFIER)) And
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
					[TemplateId],
					[TemplateTitle]
			From	[AppScript].[Template]
			Where	[TemplateId] = D.[TemplateId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FT
		Outer Apply (
			Select	Top 1
					[DataSourceId],
					[DataSourceTitle]
			From	[AppScript].[DataSource]
			Where	[DataSourceId] = D.[DataSourceId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FD
	Where	D.[TemplateId] is not null And
			D.[DataSourceId] is not null
GO