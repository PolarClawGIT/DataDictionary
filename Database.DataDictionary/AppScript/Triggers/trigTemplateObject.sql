CREATE TRIGGER [AppScript].[trigTemplateObject]
	ON [AppScript].[TemplateObject]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
