CREATE TRIGGER [AppModel].[trigEntityAttribute]
	ON [AppModel].[EntityAttribute]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END