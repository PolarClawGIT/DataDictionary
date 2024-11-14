CREATE TRIGGER [AppCatalog].[trigSchema]
	ON [AppCatalog].[Schema]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
		Exec [AppGeneral].[procRecordTransactionLog]
	END
