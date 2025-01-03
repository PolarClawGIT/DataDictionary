CREATE TRIGGER [AppModel].[trigAliasHierarchy]
	ON [AppModel].[AliasHierarchy]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
