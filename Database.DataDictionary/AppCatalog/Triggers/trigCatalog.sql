CREATE TRIGGER [AppCatalog].[trigCatalog]
	ON [AppCatalog].[Catalog]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
		Exec [AppGeneral].[procRecordTransactionLog]
	END
