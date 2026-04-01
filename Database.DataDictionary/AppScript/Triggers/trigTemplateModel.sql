CREATE TRIGGER [AppScript].[trigTemplateModel]
	ON [AppScript].[TemplateModel]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
