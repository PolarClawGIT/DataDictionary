CREATE FUNCTION [AppSecurity].[funcSecurityPrinciple] ()
Returns Table With SchemaBinding
As Return 
	Select	Convert(Bit, 1) As [IsAllowed]
	Where	Is_RoleMember('db_owner') = 1 And
			Is_RoleMember('DataDictionaryApp') = 0
	Union
	Select	Convert(Bit, 1) As [IsAllowed]
	From	[AppSecurity].[SecurityPrinciple] P
			Inner Join [AppSecurity].[SecurityMembership] M
			On	P.[PrincipleId] = M.[PrincipleId]
			Inner Join [AppSecurity].[SecurityRole] R
			On	M.[RoleId] = R.[RoleId]
	Where	Is_RoleMember('DataDictionaryApp') = 1 And
			P.[PrincipleLogin] = original_login() And
			R.[IsSecurityAdmin] = 1
GO