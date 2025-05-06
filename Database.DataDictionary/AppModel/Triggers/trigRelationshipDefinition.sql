CREATE TRIGGER [AppModel].[trigRelationshipDefinition]
	ON [AppModel].[RelationshipDefinition]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
