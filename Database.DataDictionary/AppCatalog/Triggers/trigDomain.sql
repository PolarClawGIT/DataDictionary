CREATE TRIGGER [AppCatalog].[trigDomain]
	ON [AppCatalog].[Domain]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
		Exec [AppGeneral].[procRecordTransactionLog]
	END
