CREATE FUNCTION [AppSecurity].[funcSecurityPrinciple] ()
Returns Table With SchemaBinding
As Return 
	Select	Convert(Bit, 1) As [IsAllowed]
	From	[AppSecurity].[funcSecurityPermisson](Null)
	Where	[IsDbWriter] = 1 Or
			[IsSecurityAdmin] = 1
GO