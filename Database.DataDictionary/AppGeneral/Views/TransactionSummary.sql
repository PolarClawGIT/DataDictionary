CREATE VIEW [AppGeneral].[TransactionSummary] As
Select	[TransactionDate] As [ModifiedOn], -- PK, Join to SysStart and SysEnd
		String_Agg([OriginalLogin], ', ') As [ModifiedBy]
From	[AppGeneral].[TransactionLog]
Group By [TransactionDate]
GO
