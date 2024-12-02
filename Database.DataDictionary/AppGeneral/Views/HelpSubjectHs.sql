CREATE VIEW [AppGeneral].[HelpSubjectHs]  As
-- Temporal View
Select	D.[HelpId], -- IE, PK
		D.[HelpSubject], -- IE
		D.[HelpToolTip],
		D.[HelpText],
		D.[NameSpace], --IE
		-- Temporal Status
		D.[SysStart], -- PK
		D.[SysEnd],
		C.[ModifiedOn] As [CreatedOn],
		C.[ModifiedBy] As [CreatedBy],
		R.[ModifiedOn] As [RemovedOn],
		R.[ModifiedBy] As [RemovedBy],
		Convert(Bit, IIF([PriorDate] is Null Or [PriorDate] <> D.[SysStart],1,0)) As [IsInserted],
		Convert(Bit, IIF([PriorDate] = D.[SysStart], 1, 0)) As [IsUpdated],
		Convert(Bit, IIF([NextDate] <> D.[SysEnd], 1, 0)) As [IsDeleted],
		Convert(Bit, IIF(SysUtcDateTime() >= D.[SysStart] And SysUtcDateTime() < D.[SysEnd], 1, 0)) As [IsCurrent]
From	[AppGeneral].[HelpSubject] D
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[HsGeneral].[HelpSubject]
			Where	[HelpId] = D.[HelpId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[HsGeneral].[HelpSubject]
			Where	[HelpId] = D.[HelpId] And
					[SysStart] >= D.[SysEnd]) N
		Left Join [AppGeneral].[TransactionSummary] C
		On	D.[SysStart] = C.[ModifiedOn]
		Left Join [AppGeneral].[TransactionSummary] R
		On	D.[SysEnd] = R.[ModifiedOn]
GO