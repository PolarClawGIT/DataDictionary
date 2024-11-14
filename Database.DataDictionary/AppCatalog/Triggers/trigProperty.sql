CREATE TRIGGER [AppCatalog].[trigProperty]
	ON [AppCatalog].[Property]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
		Exec [AppGeneral].[procRecordTransactionLog]
	END
