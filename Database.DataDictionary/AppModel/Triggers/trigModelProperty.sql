CREATE TRIGGER [AppModel].[trigModelProperty]
	ON [AppModel].[ModelProperty]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
