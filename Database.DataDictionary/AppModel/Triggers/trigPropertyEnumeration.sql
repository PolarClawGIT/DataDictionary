CREATE TRIGGER [AppModel].[trigPropertyEnumeration]
	ON [AppModel].[PropertyEnumeration]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
