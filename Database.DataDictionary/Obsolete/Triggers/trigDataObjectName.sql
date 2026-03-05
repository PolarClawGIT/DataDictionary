CREATE TRIGGER [Obsolete].[trigDataObjectName]
	ON [Obsolete].[DataObjectName]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
