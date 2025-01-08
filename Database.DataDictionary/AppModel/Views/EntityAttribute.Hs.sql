CREATE VIEW [AppModel].[EntityAttributeHs] As
-- Temporal View
Select	D.[EntityId], --AK
		FA.[EntityTitle],
		D.[EntityAttributeId], --PK
		D.[AttributeAlias], -- AK
		D.[AttributeName],
		D.[OrdinalPosition],
		D.[IsNullable],
		D.[IsPrimaryKey],
		-- Temporal Status
		D.[SysStart], -- PK, AK
		D.[SysEnd],
		C.[ModifiedOn] As [CreatedOn],
		C.[ModifiedBy] As [CreatedBy],
		R.[ModifiedOn] As [RemovedOn],
		R.[ModifiedBy] As [RemovedBy],
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] <> D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] <> D.[SysEnd], 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[AppModel].[EntityAttribute] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[HsModel].[EntityAttribute]
			Where	[EntityId] = D.[EntityId] And
					[EntityAttributeId] = D.[EntityAttributeId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[HsModel].[EntityAttribute]
			Where	[EntityId] = D.[EntityId] And
					[EntityAttributeId] = D.[EntityAttributeId] And
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
					[EntityId],
					[EntityTitle]
			From	[AppModel].[Entity]
			Where	[EntityId] = D.[EntityId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FA
GO