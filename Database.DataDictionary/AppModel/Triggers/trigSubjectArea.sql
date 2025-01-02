CREATE TRIGGER [AppModel].[trigSubjectArea]
	ON [AppModel].[SubjectArea]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
