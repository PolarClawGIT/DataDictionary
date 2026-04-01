CREATE TRIGGER [AppScript].[trigSchemaNode]
	ON [AppScript].[SchemaNode]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
