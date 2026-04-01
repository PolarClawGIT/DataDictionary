CREATE TRIGGER [Obsolete].[trigDocumentFile]
	ON [Obsolete].[DocumentFile]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
