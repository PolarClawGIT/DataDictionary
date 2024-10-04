CREATE SECURITY POLICY [AppSecurity].[policySecurityHelpSubject]
    ADD BLOCK PREDICATE [AppSecurity].[funcSecurityHelpSubject]([HelpId], 1)
		ON [AppGeneral].[HelpSubject] AFTER INSERT,
    ADD BLOCK PREDICATE [AppSecurity].[funcSecurityHelpSubject]([HelpId], 0)
		ON [AppGeneral].[HelpSubject] BEFORE UPDATE,
    ADD BLOCK PREDICATE [AppSecurity].[funcSecurityHelpSubject]([HelpId], 1)
		ON [AppGeneral].[HelpSubject] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO