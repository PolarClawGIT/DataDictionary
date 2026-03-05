CREATE TRIGGER [Obsolete].[trigDataObject]
	ON [Obsolete].[DataObject]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
