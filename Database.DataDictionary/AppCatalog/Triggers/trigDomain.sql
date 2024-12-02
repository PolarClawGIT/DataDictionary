CREATE TRIGGER [AppCatalog].[trigDomain]
	ON [AppCatalog].[Domain]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
