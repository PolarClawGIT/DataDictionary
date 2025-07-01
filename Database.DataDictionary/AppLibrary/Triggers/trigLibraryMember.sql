CREATE TRIGGER [AppLibrary].[trigLibraryMember]
	ON [AppLibrary].[LibraryMember]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	-- Set Transaction Log
	Exec [AppGeneral].[procRecordTransactionLog] @ProcId = @@ProcId
	END
