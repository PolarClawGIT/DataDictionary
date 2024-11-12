CREATE VIEW [AppGeneral].[HelpSubjectAK]  As
-- Temporal View
Select	D.[HelpId], -- IE, PK
		D.[HelpSubject], -- IE
		D.[HelpToolTip],
		D.[HelpText],
		D.[NameSpace], --IE
		D.[CreatedBy],
		D.[SysStart], -- IE, PK
		D.[SysEnd],
		-- Temporal Status
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
GO