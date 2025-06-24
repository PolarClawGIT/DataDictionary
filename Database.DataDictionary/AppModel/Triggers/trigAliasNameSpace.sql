CREATE TRIGGER [AppModel].[trigAliasNameSpace]
	ON [AppModel].[AliasNameSpace]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
