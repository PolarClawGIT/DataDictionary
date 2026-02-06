CREATE TRIGGER [AppScript].[trigTemplateFile]
	ON [AppScript].[TemplateFile]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
