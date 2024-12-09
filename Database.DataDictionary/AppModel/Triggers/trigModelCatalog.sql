CREATE TRIGGER [AppModel].[trigModelCatalog]
	ON [AppModel].[ModelCatalog]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
