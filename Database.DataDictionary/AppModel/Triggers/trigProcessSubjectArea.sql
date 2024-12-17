CREATE TRIGGER [AppModel].[trigProcessSubjectArea]
	ON [AppModel].[ProcessSubjectArea]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
