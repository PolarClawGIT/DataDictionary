CREATE TRIGGER [AppScript].[trigTemplateAttribute]
	ON [AppScript].[TemplateAttribute]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
