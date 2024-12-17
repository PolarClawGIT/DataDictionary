CREATE TRIGGER [AppModel].[trigEntitySubjectArea]
	ON [AppModel].[EntitySubjectArea]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
