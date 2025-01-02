CREATE TRIGGER [AppModel].[trigEntityProperty]
	ON [AppModel].[EntityProperty]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END