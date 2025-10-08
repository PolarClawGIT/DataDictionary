CREATE TRIGGER [AppScript].[trigTemplateNodeOwner]
	ON [AppScript].[TemplateNodeOwner]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
