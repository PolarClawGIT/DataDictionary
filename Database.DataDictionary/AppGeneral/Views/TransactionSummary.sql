CREATE VIEW [AppGeneral].[TransactionSummary] As
/*
** Resolves the ModifiedOn/ModifiedBy from the Transaction information.
** While unlikely, it is possible that multiple changes where applied
** on exactly the same Transaction Date.
** When this occurs, the values are aggregated such that only one row
** per date is returned.
** The Joins should be on SysStart (created aka Inserted/Deleted) or
** SysEnd (removed aka Updated/Deleted) to ModifiedOn.
*/
Select	[TransactionDate] As [ModifiedOn], -- PK, Join to SysStart and SysEnd
		String_Agg([OriginalLogin], ', ') As [ModifiedBy]
From	[AppGeneral].[TransactionLog]
Group By [TransactionDate]
GO
