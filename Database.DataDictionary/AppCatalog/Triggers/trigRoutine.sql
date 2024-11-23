CREATE TRIGGER [AppCatalog].[trigRoutine]
	ON [AppCatalog].[Routine]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
