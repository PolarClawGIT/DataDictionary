CREATE VIEW [AppGeneral].[HelpSubjectAK]  As
-- Temporal View
Select	D.[HelpId], -- IE, PK
		D.[HelpSubject], -- IE
		D.[HelpToolTip],
		D.[HelpText],
		D.[NameSpace], --IE
		D.[ModifiedBy], --IE
		D.[SysStart], -- IE, PK
		D.[SysEnd],
		-- Has values only if For System_Time All. Used to determine Inserted/Updated/Deleted.
		Max(D.[SysEnd]) Over (
			Partition By D.[HelpId]
			Order By D.[SysEnd]
			Rows Between 1 Preceding and 1 Preceding) As [PriorDate],
		Min(D.[SysStart]) Over (
			Partition by D.[HelpId]
			Order By D.[SysStart]
			Rows Between 1 Following and 1 Following) As [NextDate]
From	[AppGeneral].[HelpSubject] D
GO