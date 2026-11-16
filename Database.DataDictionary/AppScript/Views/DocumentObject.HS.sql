CREATE VIEW [AppScript].[DocumentObjectHS] AS
-- Temporal View
With [Dates] As (
	Select	[DocumentId],
			[ObjectId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[DocumentObject]
	Union
	Select	[DocumentId],
			[ObjectId],
			[SysStart],
			[SysEnd]
	From	[HsScript].[DocumentObject]
	Where	[SysStart] != [SysEnd])
Select	D.[DocumentId], -- PK
		FP.[TemplateId],
		D.[ObjectId],
		FO.[ObjectScope],
		FO.[ObjectMember],
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
From	[AppScript].[DocumentObject] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[DocumentId] = D.[DocumentId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[DocumentId] = D.[DocumentId] And
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
					[TemplateId]
			From	[AppScript].[Document]
			Where	[DocumentId] = D.[DocumentId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FP
		Outer Apply (
			Select	Top 1
					[ObjectId],
					[ObjectScope],
					[ObjectMember],
					[IsExcluded],
					[KeepOrphaned]
			From	[AppScript].[TemplateObject]
			Where	[ObjectId] = D.[ObjectId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FO
GO

