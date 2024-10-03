CREATE SECURITY POLICY [AppSecurity].[policySecurityHelpSubject]
    ADD BLOCK PREDICATE [AppSecurity].[funcSecurityHelpSubject]([HelpId])
		ON [AppGeneral].[HelpSubject]
		WITH (STATE = ON, SCHEMABINDING = ON)
GO