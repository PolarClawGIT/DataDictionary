CREATE TRIGGER [AppScript].[trigTemplateAttributeOwner]
	ON [AppScript].[TemplateAttributeOwner]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
