CREATE FUNCTION [AppSecurity].[funcSecurityHelpSubject] (@HelpId UniqueIdentifier, @OwnerOnly Bit)
Returns Table With SchemaBinding
As Return 
	Select	Convert(Bit, 1) As [IsAllowed]
	From	[AppSecurity].[funcSecurityPrincipalPermisson](@HelpId)
	Where	[IsDbWriter] = 1 Or
			[IsHelpAdmin] = 1 Or
			([IsHelpOwner] = 1 And [IsOwner] = 1) Or
			([IsGrant] = 1 And [IsDeny] = 0 And IsNull(@OwnerOnly,0) = 0)
GO