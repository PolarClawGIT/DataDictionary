CREATE TRIGGER [AppModel].[trigRelationshipSubjectArea]
	ON [AppModel].[RelationshipSubjectArea]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
