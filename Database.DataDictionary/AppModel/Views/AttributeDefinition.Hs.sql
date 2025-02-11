CREATE VIEW [AppModel].[AttributeDefinitionHs] As
-- Temporal View
With [Dates] As (
	Select	[AttributeId],
			[DefinitionId],
			[SysStart],
			[SysEnd]
	From	[AppModel].[AttributeDefinition]
	Union
	Select	[AttributeId],
			[DefinitionId],
			[SysStart],
			[SysEnd]
	From	[HsModel].[AttributeDefinition]
	Where	[SysStart] != [SysEnd])
Select	D.[AttributeId], --PK, AK
		D.[DefinitionId], -- PK
		FA.[AttributeTitle],
		FD.[DefinitionTitle], --AK
		D.[DefinitionSummary],
		D.[DefinitionText],
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
From	[AppModel].[AttributeDefinition] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[AttributeId] = D.[AttributeId] And
					[DefinitionId] = D.[DefinitionId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[AttributeId] = D.[AttributeId] And
					[DefinitionId] = D.[DefinitionId] And
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
					[AttributeId],
					[AttributeTitle]
			From	[AppModel].[Attribute]
			Where	[AttributeId] = D.[AttributeId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FA
		Outer Apply (
			Select	Top 1
					[DefinitionId],
					[DefinitionTitle]
			From	[AppModel].[DefinitionEnumeration]
			Where	[DefinitionId] = D.[DefinitionId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FD