CREATE FUNCTION [AppSecurity].[funcPrincipleAuthorization] ()
Returns Table With SchemaBinding
As Return 
	Select	Convert(Bit, 1) As [IsAllowed]
	From	[AppSecurity].[funcAuthorization](Null)
	Where	[IsDbWriter] = 1 Or
			[IsSecurityAdmin] = 1
GO