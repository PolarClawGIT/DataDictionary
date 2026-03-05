CREATE TRIGGER [Obsolete].[trigTemplateParentNode]
	ON [Obsolete].[TemplateNodeOwner]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
