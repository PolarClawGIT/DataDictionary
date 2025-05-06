CREATE TRIGGER [AppModel].[trigProcessDefinition]
	ON [AppModel].[ProcessDefinition]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
