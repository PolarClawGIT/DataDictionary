CREATE TRIGGER [AppModel].[trigModelDefinition]
	ON [AppModel].[ModelDefinition]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
