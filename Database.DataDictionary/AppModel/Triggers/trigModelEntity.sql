CREATE TRIGGER [AppModel].[trigModelEntity]
	ON [AppModel].[ModelEntity]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
