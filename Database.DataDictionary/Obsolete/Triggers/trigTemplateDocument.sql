CREATE TRIGGER [Obsolete].[trigTemplateDocument]
	ON [Obsolete].[TemplateDocument]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
