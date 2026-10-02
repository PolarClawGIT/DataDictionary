CREATE VIEW [AppScript].[SchemaDocumentHS] AS
-- Temporal View
With [Dates] As (
	Select	[DocumentId],
			[SysStart],
			[SysEnd]
	From	[AppScript].[SchemaDocument]
	Union
	Select	[DocumentId],
			[SysStart],
			[SysEnd]
	From	[HsScript].[SchemaDocument]
	Where	[SysStart] != [SysEnd])
Select	D.[DocumentId], -- PK
		FS.[TemplateId], -- AK
		D.[SchemaId],
		D.[DataFileName], -- AK
		-- Useful Data
		FO.[ObjectId],
		FO.[ObjectScope],
		FO.[ObjectMember],
		FO.[IsExcluded],
		FO.[KeepOrphaned],
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
From	[AppScript].[SchemaDocument] D
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
					[TemplateId],
					[SchemaId]
			From	[AppScript].[SchemaDefinition]
			Where	[SchemaId] = D.[SchemaId] And
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FS
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

