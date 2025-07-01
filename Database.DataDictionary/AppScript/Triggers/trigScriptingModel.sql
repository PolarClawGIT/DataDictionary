CREATE TRIGGER [AppScript].[trigScriptingModel]
	ON [AppScript].[ScriptingModel]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
