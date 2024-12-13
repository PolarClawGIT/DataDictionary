CREATE TRIGGER [AppModel].[trig[SubjectArea]
	ON [AppModel].[SubjectArea]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
