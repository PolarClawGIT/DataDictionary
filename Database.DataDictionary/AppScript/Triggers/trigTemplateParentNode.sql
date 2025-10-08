CREATE TRIGGER [AppScript].[trigTemplateParentNode]
	ON [AppScript].[TemplateParentNode]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
