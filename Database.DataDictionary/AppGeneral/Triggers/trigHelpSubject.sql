CREATE TRIGGER [AppGeneral].[trigHelpSubject]
	ON [AppGeneral].[HelpSubject]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
		Exec [AppGeneral].[procRecordTransactionLog]
	END
