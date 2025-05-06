CREATE TRIGGER [AppModel].[trigRelationship]
	ON [AppModel].[Relationship]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
