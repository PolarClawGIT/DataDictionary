CREATE TRIGGER [AppCatalog].[trigCatalogModel]
	ON [AppCatalog].[CatalogModel]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
