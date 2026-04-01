/* Obsolete

CREATE TRIGGER [AppScript].[trigScriptingNameSpace]
	ON [AppScript].[ScriptingNameSpace]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
	*/
