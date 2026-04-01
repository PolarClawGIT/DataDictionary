CREATE TRIGGER [AppScript].[trigSchemaDefinition]
	ON [AppScript].[SchemaDefinition]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
