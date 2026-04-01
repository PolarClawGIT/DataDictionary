CREATE TRIGGER [Obsolete].[trigTemplate]
	ON [Obsolete].[Template]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
