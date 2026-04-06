CREATE TRIGGER [AppScript].[trigSchemaDocument]
	ON [AppScript].[SchemaDocument]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
