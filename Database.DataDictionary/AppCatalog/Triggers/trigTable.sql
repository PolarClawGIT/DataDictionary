CREATE TRIGGER [AppCatalog].[trigTable]
	ON [AppCatalog].[Table]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
