CREATE FUNCTION [AppSecurity].[funcApplicationAuthorization] ()
Returns Table With SchemaBinding as Return
-- Row Level Security
-- Only the Application, db_datawriter, or db_owner can modify the rows.
-- The Application uses stored procedures and does further checks to verify Authorization.
-- One (granted) or zero (denied) rows are returned.
Select	[PrincipalLogin],
		[IsApplication],
		[IsDbWriter]
From	[AppSecurity].[funcAuthorization](Null)
Where	[IsApplication] = 1 Or
		[IsDbWriter] = 1
GO