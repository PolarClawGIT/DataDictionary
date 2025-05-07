CREATE VIEW [AppModel].[RelationshipAliasHs] As
-- Temporal View
With [Dates] As (
	Select	[RelationshipId],
			[AliasId],
			[SysStart],
			[SysEnd]
	From	[AppModel].[RelationshipAlias]
	Union
	Select	[RelationshipId],
			[AliasId],
			[SysStart],
			[SysEnd]
	From	[HsModel].[RelationshipAlias]
	Where	[SysStart] != [SysEnd])
Select	D.[RelationshipId],
		D.[AliasId],
		FA.[RelationshipTitle],
		D.[AliasScope],
		FL.[AliasPath],
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
From	[AppModel].[RelationshipAlias] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[RelationshipId] = D.[RelationshipId] And
					[AliasId] = D.[AliasId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[RelationshipId] = D.[RelationshipId] And
					[AliasId] = D.[AliasId] And
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
					[RelationshipId],
					[RelationshipTitle]
			From	[AppModel].[Relationship]
			Where	[RelationshipId] = D.[RelationshipId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FA
		Outer Apply (
			Select	Top 1
					[AliasId],
					[AliasNameSpace] As [AliasPath]
			From	[AppModel].[AliasHS]
			Where	[AliasId] = D.[AliasId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FL
GO