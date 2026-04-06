CREATE TRIGGER [AppScript].[trigTransformDocument]
	ON [AppScript].[TransformDocument]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
