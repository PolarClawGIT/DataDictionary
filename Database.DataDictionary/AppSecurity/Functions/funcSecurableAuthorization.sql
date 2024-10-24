CREATE FUNCTION [AppSecurity].[funcSecurableAuthorization] (@SecurableId UniqueIdentifier)
Returns Table With SchemaBinding
As Return 
	Select	Convert(Bit, 1) As [IsAllowed]
	From	[AppSecurity].[funcAuthorization](@SecurableId)
	Where	[IsDbWriter] = 1 Or
			[IsSecurityAdmin] = 1 Or
			[IsOwner] = 1
Go