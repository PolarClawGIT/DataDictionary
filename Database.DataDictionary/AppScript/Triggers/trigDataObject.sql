CREATE TRIGGER [AppScript].[trigDataObject]
	ON [AppScript].[DataObject]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
