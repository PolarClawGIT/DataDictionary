CREATE TRIGGER [AppModel].[trigAttributeAlias]
	ON [AppModel].[AttributeAlias]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
