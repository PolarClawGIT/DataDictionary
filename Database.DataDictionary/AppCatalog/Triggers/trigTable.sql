CREATE TRIGGER [AppCatalog].[trigTable]
	ON [AppCatalog].[Table]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
		Exec [AppGeneral].[procRecordTransactionLog]
	END
