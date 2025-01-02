CREATE VIEW [AppModel].[AttributeHs] As
-- Temporal View
Select	D.[AttributeId], -- PK
		D.[AttributeTitle], -- AK
		D.[AttributeDescription],
		D.[AttributeName],
		D.[IsSingleValue],
		Convert(Bit, Case D.[IsSingleValue] When 1 Then 0 When 0 Then 1 Else Null End) As [IsMultiValue],
		D.[IsSimpleType],
		Convert(Bit, Case D.[IsSimpleType] When 1 Then 0 When 0 Then 1 Else Null End) As [IsCompositeType],
		Convert(Bit, Case D.[IsIntegral] When 1 Then 0 When 0 Then 1 Else Null End) As [IsDerived],
		D.[IsIntegral],
		D.[IsNullable],
		Convert(Bit, Case D.[IsNullable] When 1 Then 0 When 0 Then 1 Else Null End) As [IsValued],
		D.[IsKey],
		Convert(Bit, Case D.[IsKey] When 1 Then 0 When 0 Then 1 Else Null End) As [IsNonKey],
		-- Temporal Status
		D.[SysStart], -- AK, PK
		D.[SysEnd],
		C.[ModifiedOn] As [CreatedOn],
		C.[ModifiedBy] As [CreatedBy],
		R.[ModifiedOn] As [RemovedOn],
		R.[ModifiedBy] As [RemovedBy],
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] <> D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] <> D.[SysEnd], 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[AppModel].[Attribute] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[HsModel].[Attribute]
			Where	[AttributeId] = D.[AttributeId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[HsModel].[Attribute]
			Where	[AttributeId] = D.[AttributeId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO