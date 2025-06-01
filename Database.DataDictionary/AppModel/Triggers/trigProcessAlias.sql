CREATE TRIGGER [AppModel].[trigProcessAlias]
	ON [AppModel].[ProcessAlias]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
