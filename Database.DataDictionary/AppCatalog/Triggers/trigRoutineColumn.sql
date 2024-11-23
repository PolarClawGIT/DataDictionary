CREATE TRIGGER [AppCatalog].[trigRoutineColumn]
	ON [AppCatalog].[RoutineColumn]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
