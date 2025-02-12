CREATE VIEW [AppGeneral].[HelpSubjectHs]  As
-- Temporal View
With [Dates] As (
	Select	[HelpId],
			[SysStart],
			[SysEnd]
	From	[AppGeneral].[HelpSubject]
	Union
	Select	[HelpId],
			[SysStart],
			[SysEnd]
	From	[HsGeneral].[HelpSubject]
	Where	[SysStart] != [SysEnd])
Select	D.[HelpId], -- IE, PK
		D.[HelpSubject], -- IE
		D.[HelpToolTip],
		D.[HelpText],
		D.[NameSpace], --IE
		-- Temporal Status
		D.[SysStart], -- PK
		D.[SysEnd],
		IsNull(C.[ModifiedOn], D.[SysStart]) As [CreatedOn],
		C.[ModifiedBy] As [CreatedBy],
		IsNull(R.[ModifiedOn], NullIf(D.[SysEnd],'9999-12-31 23:59:59.9999999')) As [RemovedOn],
		R.[ModifiedBy] As [RemovedBy],
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] <> D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] is Null And D.[SysEnd] < SysUtcDateTime(), 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[AppGeneral].[HelpSubject] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[Dates]
			Where	[HelpId] = D.[HelpId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[Dates]
			Where	[HelpId] = D.[HelpId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO