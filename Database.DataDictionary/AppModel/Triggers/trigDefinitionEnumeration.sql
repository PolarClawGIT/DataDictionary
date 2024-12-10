CREATE TRIGGER [AppModel].[trigDefinitionEnumeration]
	ON [AppModel].[DefinitionEnumeration]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
