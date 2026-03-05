CREATE TRIGGER [Obsolete].[trigTemplateNode]
	ON [Obsolete].[TemplateNode]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
