CREATE VIEW [AppScript].[ScriptingAttributeHs] As
-- Temporal View
With [Dates] As (
	Select	[AttributeId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[ScriptingAttribute]
	/*Union -- TODO: Temporal not yet implemented
	Select	[AttributeId],
			[SysStart],
			[SysEnd]
	From	[HsScript].[ScriptingAttribute]
	Where	[SysStart] != [SysEnd]*/)
Select	D.[AttributeId],
		D.[NodeId],
		FT.[TemplateId],
		FT.[TemplateTitle],
		D.[AttributeName],
		D.[AttributeValue],
		D.[PropertyId],
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
From	[AppScript].[ScriptingAttribute] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[AttributeId] = D.[AttributeId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[AttributeId] = D.[AttributeId] And
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
					[NodeId],
					[TemplateId]
			From	[AppScript].[ScriptingNode]
			Where	[NodeId] = D.[NodeId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FN
		Outer Apply (
			Select	Top 1
					[TemplateId],
					[TemplateTitle]
			From	[AppScript].[ScriptingTemplate]
			Where	[TemplateId] = FN.[TemplateId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FT
GO