CREATE TRIGGER [AppCatalog].[trigReference]
	ON [AppCatalog].[Reference]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
