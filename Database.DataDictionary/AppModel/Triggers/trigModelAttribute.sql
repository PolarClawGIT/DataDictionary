CREATE TRIGGER [AppModel].[trigModelAttribute]
	ON [AppModel].[ModelAttribute]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
