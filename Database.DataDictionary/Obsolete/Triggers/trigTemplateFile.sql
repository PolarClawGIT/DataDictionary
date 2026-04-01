CREATE TRIGGER [Obsolete].[trigTemplateFile]
	ON [Obsolete].[TemplateFile]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
