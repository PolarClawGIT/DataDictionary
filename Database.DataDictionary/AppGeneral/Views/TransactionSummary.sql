CREATE VIEW [AppGeneral].[TransactionSummary] As
/*
** Resolves the ModifiedOn/ModifiedBy from the Transaction information.
**
** This takes advantage of the behavior of Temporal Table in SQL.
** The Start/End times of Temporal Tables are the UTS date/time of
** when the Transaction is Started. Because the Transaction Log table
** also stores the date using the same rules, the Start date
** is the same as the date applied to the Temporal Tables.
** If the transaction should effect multiple rows or multiple tables,
** everything gets the same date.
**
** While unlikely, it is possible that multiple changes are applied
** exactly the same Transaction Date but be part of different transactions.
** This can occur because the System UTF Date is subject to the precision/settings
** of the system clock on the server that initiated the transaction.
** With clustered and multi-core servers in a high-demand system, it can occur.
** When this occurs, the values are aggregated such that only one row per date is returned.
**
** The Joins should be on SysStart (created aka Inserted/Updated) or
** SysEnd (removed aka Updated/Deleted) to ModifiedOn.
**
** Looking forward and backwards in the History table can be used to
** determine the difference between a Insert/Update/Delete.
**
** If the prior record has an end date equal to the current record
** start date, then it was an update. Any other date or null is is an Insert.
**
** If the next record has a start date that equal the current record
** end date, then it was an update. Any other date or null it is a delete.
**
** A null prior record is just a new key (insert).
** A null next record is just the last record (update cannot be determined).
**
** When looking at history, records with the same begin and end date are records
** that where altered multiple times in the same transaction. They are normally
** discarded as they are usually an issue with how the application is updating values.
*/
Select	[TransactionDate] As [ModifiedOn], -- PK, Join to SysStart and SysEnd
		String_Agg(IsNull(P.[PrincipalName], L.[ExecuteLogin]), ', ') As [ModifiedBy]
From	[AppGeneral].[TransactionLog] L
		Left Join [AppSecurity].[Principal] P
		On	L.[ExecuteLogin] = P.[PrincipalLogin]
Group By [TransactionDate]
GO
