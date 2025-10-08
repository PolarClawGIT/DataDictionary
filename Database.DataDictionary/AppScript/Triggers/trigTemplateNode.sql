CREATE TRIGGER [AppScript].[trigTemplateNode]
	ON [AppScript].[TemplateNode]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
