CREATE FUNCTION [AppSecurity].[funcSecurityObject] (@ObjectId UniqueIdentifier, @OwnerOnly Bit)
Returns Table With SchemaBinding
As Return 
	Select	IsNull(Convert(Bit, 0),0) As [IsAllowed]
	Where	1=2 -- Sets up Column Names and Type
	Union	-- User is Db Owner executing code directly
	Select	Convert(Bit, 1) As [IsAllowed]
	Where	Is_RoleMember('db_owner') = 1 And
			Is_RoleMember('DataDictionaryApp') = 0
	Union	-- User is owner of the object
	Select	Convert(Bit, 1) As [IsAllowed]
	From	[AppSecurity].[SecurityPrinciple] P
			Inner Join [AppSecurity].[SecurityOwner] O
			On	P.[PrincipleId] = O.[PrincipleId]
	Where	Is_RoleMember('DataDictionaryApp') = 1 And
			P.[PrincipleLogin] = Original_Login() And
			O.[ObjectId] = @ObjectId
	Union	-- User granted permissions to the object
	Select	Convert(Bit, 1) As [IsAllowed]
	From	[AppSecurity].[SecurityPrinciple] P
			Inner Join [AppSecurity].[SecurityMembership] M
			On	P.[PrincipleId] = M.[PrincipleId]
			Inner Join [AppSecurity].[SecurityPermission] S
			On	M.[RoleId] = S.[RoleId]
	Where	Is_RoleMember('DataDictionaryApp') = 1 And
			P.[PrincipleLogin] = Original_Login() And
			S.[ObjectId] = @ObjectId And
			IsNull(@OwnerOnly,0) = 0 And
			S.[IsGrant] = 1
	Except -- User has been denied in any role, Revokes all other grants
	Select	Convert(Bit, 1) As [IsAllowed]
	From	[AppSecurity].[SecurityPrinciple] P
			Inner Join [AppSecurity].[SecurityMembership] M
			On	P.[PrincipleId] = M.[PrincipleId]
			Inner Join [AppSecurity].[SecurityPermission] S
			On	M.[RoleId] = S.[RoleId]
	Where	Is_RoleMember('DataDictionaryApp') = 1 And
			P.[PrincipleLogin] = Original_Login() And
			S.[ObjectId] = @ObjectId And
			S.[IsDeny] = 1
Go