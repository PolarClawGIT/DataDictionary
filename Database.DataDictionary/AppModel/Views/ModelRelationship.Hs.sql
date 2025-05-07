CREATE VIEW [AppModel].[ModelRelationshipHs] As
-- Temporal View
With [Dates] As (
	Select	[ModelId],
			[RelationshipId],
			[SysStart],
			[SysEnd]
	From	[AppModel].[ModelRelationship]
	Union
	Select	[ModelId],
			[RelationshipId],
			[SysStart],
			[SysEnd]
	From	[HsModel].[ModelRelationship]
	Where	[SysStart] != [SysEnd])
Select	D.[ModelId],
		FM.[ModelTitle],
		D.[RelationshipId],
		FA.[RelationshipTitle],
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
From	[AppModel].[ModelRelationship] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[ModelId] = D.[ModelId] And
					[RelationshipId] = D.[RelationshipId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[ModelId] = D.[ModelId] And
					[RelationshipId] = D.[RelationshipId] And
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
					[RelationshipTitle],
					[RelationshipName]
			From	[AppModel].[Relationship]
			Where	[RelationshipId] = D.[RelationshipId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FA
		Outer Apply (
			Select	Top 1
					[ModelId],
					[ModelTitle]
			From	[AppModel].[Model]
			Where	[ModelId] = D.[ModelId] and
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FM
GO
