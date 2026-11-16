CREATE TRIGGER [AppScript].[trigDocumentObject]
	ON [AppScript].[DocumentObject]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
