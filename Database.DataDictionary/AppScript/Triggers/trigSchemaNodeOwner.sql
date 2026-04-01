CREATE TRIGGER [AppScript].[trigSchemaNodeOwner]
	ON [AppScript].[SchemaNodeOwner]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
