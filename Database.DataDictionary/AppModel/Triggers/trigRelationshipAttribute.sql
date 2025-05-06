CREATE TRIGGER [AppModel].[trigRelationshipAttribute]
	ON [AppModel].[RelationshipAttribute]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
