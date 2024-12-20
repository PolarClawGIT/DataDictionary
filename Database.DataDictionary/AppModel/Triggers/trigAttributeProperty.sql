CREATE TRIGGER [AppModel].[trigAttributeProperty]
	ON [AppModel].[AttributeProperty]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
