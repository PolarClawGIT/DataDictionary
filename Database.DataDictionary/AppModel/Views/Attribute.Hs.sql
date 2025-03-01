CREATE VIEW [AppModel].[AttributeHs] As
-- Temporal View
With [Dates] As (
	Select	[AttributeId],
			[SysStart],
			[SysEnd]
	From	[AppModel].[Attribute]
	Union
	Select	[AttributeId],
			[SysStart],
			[SysEnd]
	From	[HsModel].[Attribute]
	Where	[SysStart] != [SysEnd])
Select	D.[AttributeId], -- PK
		D.[AttributeTitle], -- AK
		D.[AttributeDescription],
		D.[AttributeName],

		D.[DataType],
		D.[DataLength],
		D.[DataPrecision],
		D.[DataScale],

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
--		[PriorDate],
--		[NextDate],
		IsNull(C.[ModifiedOn], D.[SysStart]) As [CreatedOn],
		C.[ModifiedBy] As [CreatedBy],
		IsNull(R.[ModifiedOn], NullIf(D.[SysEnd],'9999-12-31 23:59:59.9999999')) As [RemovedOn],
		R.[ModifiedBy] As [RemovedBy],
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] != D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] is Null And D.[SysEnd] < SysUtcDateTime(), 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[AppModel].[Attribute] D
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
GO
