CREATE TRIGGER [AppScript].[trigTemplateNodeOwner_Old]
	ON [AppScript].[TemplateNodeOwner_Old]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
