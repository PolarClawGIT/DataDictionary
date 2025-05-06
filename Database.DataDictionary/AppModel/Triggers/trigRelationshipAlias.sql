CREATE TRIGGER [AppModel].[trigRelationshipAlias]
	ON [AppModel].[RelationshipAlias]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
