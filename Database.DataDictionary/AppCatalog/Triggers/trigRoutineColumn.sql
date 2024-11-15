CREATE TRIGGER [AppCatalog].[trigRoutineColumn]
	ON [AppCatalog].[RoutineColumn]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
		Exec [AppGeneral].[procRecordTransactionLog]
	END
