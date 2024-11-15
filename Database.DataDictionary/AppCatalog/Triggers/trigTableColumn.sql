CREATE TRIGGER [AppCatalog].[trigTableColumn]
	ON [AppCatalog].[TableColumn]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
		Exec [AppGeneral].[procRecordTransactionLog]
	END
