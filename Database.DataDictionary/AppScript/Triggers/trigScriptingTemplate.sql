CREATE TRIGGER [AppScript].[trigScriptingTemplate]
	ON [AppScript].[ScriptingTemplate]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
