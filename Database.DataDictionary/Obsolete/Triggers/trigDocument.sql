CREATE TRIGGER [Obsolete].[trigDocument]
	ON [Obsolete].[Document]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
