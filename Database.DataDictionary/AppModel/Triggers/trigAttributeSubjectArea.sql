CREATE TRIGGER [AppModel].[trigAttributeSubjectArea]
	ON [AppModel].[AttributeSubjectArea]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
