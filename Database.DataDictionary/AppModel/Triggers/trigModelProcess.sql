CREATE TRIGGER [AppModel].[trigModelProcess]
	ON [AppModel].[ModelProcess]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
