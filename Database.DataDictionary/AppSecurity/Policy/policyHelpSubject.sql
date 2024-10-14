CREATE SECURITY POLICY [AppSecurity].[policyHelpSubject]
    ADD BLOCK PREDICATE [AppSecurity].[funcHelpSubjectAuthorization]([HelpId], 1)
		ON [AppGeneral].[HelpSubject] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcHelpSubjectAuthorization]([HelpId], 0)
		ON [AppGeneral].[HelpSubject] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcHelpSubjectAuthorization]([HelpId], 1)
		ON [AppGeneral].[HelpSubject] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO