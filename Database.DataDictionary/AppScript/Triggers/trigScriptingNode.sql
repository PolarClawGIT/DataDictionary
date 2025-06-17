CREATE TRIGGER [AppScript].[trigScriptingNode]
	ON [AppScript].[ScriptingNode]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
