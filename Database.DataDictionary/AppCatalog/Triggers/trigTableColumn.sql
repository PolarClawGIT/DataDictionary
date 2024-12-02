CREATE TRIGGER [AppCatalog].[trigTableColumn]
	ON [AppCatalog].[TableColumn]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
