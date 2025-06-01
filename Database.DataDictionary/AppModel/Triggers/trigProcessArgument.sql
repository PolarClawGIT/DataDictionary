CREATE TRIGGER [AppModel].[trigProcessArgument]
	ON [AppModel].[ProcessArgument]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
