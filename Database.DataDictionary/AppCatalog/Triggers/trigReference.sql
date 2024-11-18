CREATE TRIGGER [AppCatalog].[trigReference]
	ON [AppCatalog].[Reference]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
		Exec [AppGeneral].[procRecordTransactionLog]
	END
