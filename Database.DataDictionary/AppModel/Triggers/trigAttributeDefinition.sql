CREATE TRIGGER [AppModel].[trigAttributeDefinition]
	ON [AppModel].[AttributeDefinition]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
