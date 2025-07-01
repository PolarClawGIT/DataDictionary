CREATE TRIGGER [AppScript].[trigScriptingAttribute]
	ON [AppScript].[ScriptingAttribute]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
