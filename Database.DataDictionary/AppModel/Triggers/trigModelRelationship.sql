CREATE TRIGGER [AppModel].[trigModelRelationship]
	ON [AppModel].[ModelRelationship]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
