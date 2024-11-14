CREATE TABLE [AppGeneral].[TransactionLog]
(	/*
	** The TransactionLog is used to identify the Login/User that
	** perform an action by to a logged table. This allows the system
	** to identify who perform an action.
	** [SysStart] is the UTC Date of the Transaction.
	** [SysStart] will match the [SysStart] of the logged table.
	** [SysStart] is not guaranteed to be unique with unlikely/rare duplicates.
	*/
	[TransactionId] UniqueIdentifier Not Null CONSTRAINT [DF_TransactionId] DEFAULT (newid()),
	[login_time] DateTime Not Null,  -- [sys].[dm_exec_sessions].[login_time]
	[session_id] Int Not Null, -- [sys].[dm_exec_sessions].[session_id] and @@SPID
	[transaction_id] BigInt Not Null, -- [sys].[dm_tran_session_transactions].[transaction_id]
	[OriginalLogin] SysName Not Null CONSTRAINT [DF_TransactionLog_OriginalLogin] DEFAULT (original_login()),
	[ExecuteLogin] SysName Not Null CONSTRAINT [DF_TransactionLog_ExecuteLogin] Default (suser_name()),
	[ExecuteUser] SysName Not Null CONSTRAINT [DF_TransactionLog_ExecuteUser] Default (user_name()),
	[TransactionDate] DATETIME2 (7) GENERATED ALWAYS AS ROW START NOT NULL CONSTRAINT [DF_TransactionLog_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TransactionLog_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([TransactionDate], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_TransactionLog] PRIMARY KEY CLUSTERED ([TransactionId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_TransactionLog]
    ON [AppGeneral].[TransactionLog]([login_time], [session_id], [transaction_id], [TransactionDate]);
GO