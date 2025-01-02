CREATE TRIGGER [AppModel].[trigEntityDefinition]
	ON [AppModel].[EntityDefinition]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END