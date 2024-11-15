CREATE TRIGGER [AppCatalog].[trigRoutineParameter]
	ON [AppCatalog].[RoutineParameter]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
		Exec [AppGeneral].[procRecordTransactionLog]
	END
