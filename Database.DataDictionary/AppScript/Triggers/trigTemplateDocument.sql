CREATE TRIGGER [AppScript].[trigTemplateDocument]
	ON [AppScript].[TemplateDocument]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
