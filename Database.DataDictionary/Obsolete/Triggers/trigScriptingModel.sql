CREATE TRIGGER [Obsolete].[trigScriptingModel]
	ON [Obsolete].[ScriptingModel]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
