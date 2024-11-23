CREATE TRIGGER [AppCatalog].[trigSchema]
	ON [AppCatalog].[Schema]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
