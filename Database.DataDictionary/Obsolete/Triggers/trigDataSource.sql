CREATE TRIGGER [Obsolete].[trigDataSource]
	ON [Obsolete].[DataSource]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
