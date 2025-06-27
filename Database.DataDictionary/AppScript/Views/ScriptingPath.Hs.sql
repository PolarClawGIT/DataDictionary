CREATE VIEW [AppScript].[ScriptingPathHs] As
-- Temporal View
With [Dates] As (
	Select	[TemplateId],
			[NameSpaceId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[ScriptingPath]
/*	Union -- TODO: Temporal not yet implemented
	Select	[TemplateId],
			[NameSpaceId],
			[SysStart],
			[SysEnd]
	From	[HsScript].[ScriptingPath]
	Where	[SysStart] != [SysEnd]*/)
Select	D.[TemplateId],
		D.[NameSpaceId],
		FT.[TemplateTitle],
		FN.[NameSpace],
		D.[NameSpaceScope],
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
From	[AppScript].[ScriptingPath] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[TemplateId] = D.[TemplateId] And
					[NameSpaceId] = D.[NameSpaceId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[TemplateId] = D.[TemplateId] And
					[NameSpaceId] = D.[NameSpaceId] And
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
			From	[AppScript].[ScriptingTemplate]
			Where	[TemplateId] = D.[TemplateId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FT
		Outer Apply (
			Select	Top 1
					[NameSpaceId],
					[NameSpace]
			From	[AppScript].[ScriptingNameSpaceHs]
			Where	[NameSpaceId] = D.[NameSpaceId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FN
GO