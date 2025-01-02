CREATE VIEW [AppModel].[SubjectAreaHs] AS
-- Temporal View
Select	D.[SubjectAreaId], -- PK
		D.[SubjectAreaTitle], -- AK
		D.[SubjectAreaDescription],
		S.[SubjectName],
		D.[ModelId], 
		FM.[ModelTitle],
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
From	[AppModel].[SubjectArea] D
		Cross Apply (
			Select	[QualifiedName] As [SubjectName]
			From	[AppModel].[funcParseName](D.[SubjectName])) S
		Outer Apply (
			Select	Max([SysEnd]) As [PriorDate]
			From	[HsModel].[SubjectArea]
			Where	[SubjectAreaId] = D.[SubjectAreaId] And
					[SysStart] < D.[SysStart]) P
		Outer Apply (
			Select	Min([SysStart]) As [NextDate]
			From	[HsModel].[SubjectArea]
			Where	[SubjectAreaId] = D.[SubjectAreaId] And
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
					[ModelId],
					[ModelTitle]
			From	[AppModel].[Model]
			Where	[ModelId] = D.[ModelId] and
					[SysStart] <= D.[SysEnd]
			Order By [SysStart] Desc) FM
GO