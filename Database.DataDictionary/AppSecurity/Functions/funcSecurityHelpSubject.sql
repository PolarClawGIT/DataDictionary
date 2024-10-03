CREATE FUNCTION  [AppSecurity].[funcSecurityHelpSubject] (@HelpId UniqueIdentifier)
Returns Table With SchemaBinding
As Return 
	Select	Convert(Bit, 1) As [IsAllowed]
	Where	Is_RoleMember('db_owner') = 1 And
			Is_RoleMember('DataDictionaryApp') = 0
	Union
	Select	Distinct
			Convert(Bit, 1) As [IsAllowed]
	From	[AppSecurity].[SecurityPrinciple] P
			Inner Join [AppSecurity].[SecurityMembership] M
			On	P.[PrincipleId] = M.[PrincipleId]
			Inner Join [AppSecurity].[SecurityRole] R
			On	M.[RoleId] = R.[RoleId]
			Left Join [AppSecurity].[SecurityPermission] S
			On	R.[RoleId] = S.[RoleId] 
			Left Join [AppGeneral].[HelpSubject] T
			On	S.[SystemId] = T.[HelpId]
	Where	Is_RoleMember('DataDictionaryApp') = 1 And
			P.[PrincipleLogin] = original_login() And
			(R.[IsHelpAdmin] = 1 Or 
			(S.[IsWrite] = 1 And T.[HelpId] = @HelpId))
GO