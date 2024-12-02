CREATE TRIGGER [AppCatalog].[trigRoutineParameter]
	ON [AppCatalog].[RoutineParameter]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
