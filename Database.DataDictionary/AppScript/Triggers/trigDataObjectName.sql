CREATE TRIGGER [AppScript].[trigDataObjectName]
	ON [AppScript].[DataObjectName]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
