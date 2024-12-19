CREATE SECURITY POLICY [dbo].[policySecurityExample]
    ADD BLOCK PREDICATE [dbo].[funcSecurityDeny] ()
		ON [dbo].[SecurityExample] AFTER INSERT,
    ADD BLOCK PREDICATE [dbo].[funcSecurityDeny] ()
		ON [dbo].[SecurityExample] BEFORE UPDATE,
    ADD BLOCK PREDICATE [dbo].[funcSecurityDeny] ()
		ON [dbo].[SecurityExample] BEFORE DELETE
	WITH (STATE = ON, SCHEMABINDING = ON)
GO
