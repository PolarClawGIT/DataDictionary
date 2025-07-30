CREATE TRIGGER [AppScript].[trigTemplateElement]
	ON [AppScript].[TemplateElement]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
