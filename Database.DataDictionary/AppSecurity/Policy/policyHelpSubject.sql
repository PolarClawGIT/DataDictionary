CREATE SECURITY POLICY [AppSecurity].[policyHelpSubject]
	ADD BLOCK PREDICATE [AppSecurity].[funcHelpSubjectAuthorization] ([HelpId], 1) ON [AppGeneral].[HelpSubject]
	WITH (STATE = ON, SCHEMABINDING = ON)
GO