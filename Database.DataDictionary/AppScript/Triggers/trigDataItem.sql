CREATE TRIGGER [AppScript].[trigDataItem]
	ON [AppScript].[DataItem]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
