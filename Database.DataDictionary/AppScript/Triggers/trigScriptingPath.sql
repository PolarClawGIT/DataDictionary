/* Obsolete
CREATE TRIGGER [AppScript].[trigScriptingPath]
	ON [AppScript].[ScriptingPath]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
*/