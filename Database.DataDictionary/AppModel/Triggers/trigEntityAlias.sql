CREATE TRIGGER [AppModel].[trigEntityAlias]
	ON [AppModel].[EntityAlias]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END