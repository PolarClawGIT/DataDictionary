CREATE FUNCTION [AppSecurity].[funcSecurityHelpSubject] (@HelpId UniqueIdentifier, @OwnerOnly Bit)
Returns Table With SchemaBinding
As Return 
	Select	IsNull(Convert(Bit, 0),0) As [IsAllowed]
	Where	1=2 -- Sets up Column Names and Type
	Union	-- User is Db Owner executing code directly
	Select	Convert(Bit, 1) As [IsAllowed]
	Where	Is_RoleMember('db_owner') = 1 And
			Is_RoleMember('DataDictionaryApp') = 0
	Union	-- Does the user have security to the Object
	Select	Convert(Bit, 1) As [IsAllowed]
	From	[AppSecurity].[funcSecurityPrincipalPermisson](@HelpId)
	Where	[IsHelpAdmin] = 1 Or
			([IsHelpOwner] = 1 And [IsOwner] = 1) Or
			([IsGrant] = 1 And [IsDeny] = 0 And IsNull(@OwnerOnly,0) = 0)
GO