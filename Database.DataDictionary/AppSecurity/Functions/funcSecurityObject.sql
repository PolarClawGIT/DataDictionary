CREATE FUNCTION [AppSecurity].[funcSecurityObject] (@ObjectId UniqueIdentifier)
Returns Table With SchemaBinding
As Return 
	Select	Convert(Bit, 1) As [IsAllowed]
	From	[AppSecurity].[funcSecurityPrincipalPermisson](@ObjectId)
	Where	[IsDbWriter] = 1 Or
			[IsSecurityAdmin] = 1 Or
			[IsOwner] = 1
Go