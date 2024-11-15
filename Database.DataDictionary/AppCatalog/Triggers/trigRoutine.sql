CREATE TRIGGER [AppCatalog].[trigRoutine]
	ON [AppCatalog].[Routine]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
		Exec [AppGeneral].[procRecordTransactionLog]
	END
