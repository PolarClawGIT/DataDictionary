CREATE TRIGGER [AppCatalog].[trigProperty]
	ON [AppCatalog].[Property]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
