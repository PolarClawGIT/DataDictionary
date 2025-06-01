CREATE TRIGGER [AppModel].[trigProcessProperty]
	ON [AppModel].[ProcessProperty]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
