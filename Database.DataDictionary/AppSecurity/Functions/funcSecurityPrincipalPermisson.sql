CREATE FUNCTION [AppSecurity].[funcSecurityPrincipalPermisson] (@ObjectId UniqueIdentifier = null)
Returns Table With SchemaBinding
As Return
-- Master Security Function.
-- All other Security Functions use this function.
With [Login] As (
	-- Database Level security
	Select	Original_Login() As [PrincipleLogin],
			Convert(Bit, IIF(
				Is_RoleMember('DataDictionaryApp') = 0 And -- Cannot be executing using the application
				Is_RoleMember('db_denydatawriter') = 0 And
				(Is_RoleMember('db_datawriter') = 1 Or
				 Is_RoleMember('db_owner') = 1), 1, 0)) As [IsDbWriter])
Select	L.[PrincipleLogin],
		P.[PrincipleId],
		@ObjectId As [ObjectId],
		Convert(Bit,Min(Convert(Int,L.[IsDbWriter]))) As [IsDbWriter],
		Convert(Bit,Max(Convert(Int,IsNull(R.[IsSecurityAdmin],0)))) As [IsSecurityAdmin],
		Convert(Bit,Max(Convert(Int,IsNull(R.[IsHelpAdmin],0)))) As [IsHelpAdmin],
		Convert(Bit,Max(Convert(Int,IsNull(R.[IsHelpOwner],0)))) As [IsHelpOwner],
		Convert(Bit,Max(Convert(Int,IsNull(R.[IsCatalogAdmin],0)))) As [IsCatalogAdmin],
		Convert(Bit,Max(Convert(Int,IsNull(R.[IsCatalogOwner],0)))) As [IsCatalogOwner],
		Convert(Bit,Max(Convert(Int,IsNull(R.[IsLibraryAdmin],0)))) As [IsLibraryAdmin],
		Convert(Bit,Max(Convert(Int,IsNull(R.[IsLibraryOwner],0)))) As [IsLibraryOwner],
		Convert(Bit,Max(Convert(Int,IsNull(R.[IsModelAdmin],0)))) As [IsModelAdmin],
		Convert(Bit,Max(Convert(Int,IsNull(R.[IsModelOwner],0)))) As [IsModelOwner],
		Convert(Bit,Max(Convert(Int,IsNull(R.[IsScriptOwner],0)))) As [IsScriptAdmin],
		Convert(Bit,Max(Convert(Int,IsNull(R.[IsScriptOwner],0)))) As [IsScriptOwner],
		Convert(Bit,Max(Convert(Int,IsNull(S.[IsGrant],0)))) As [IsGrant],
		Convert(Bit,Max(Convert(Int,IsNull(S.[IsDeny],0)))) As [IsDeny],
		Convert(Bit,Max(IIF(O.[PrincipleId] = P.[PrincipleId],1,0))) As [IsOwner],
		Convert(Bit,Max(IIF(O.[ObjectId] is Null, 0, 1))) As [HasOwner]
From	[Login] L
		-- Application Level Security
		Left Join[AppSecurity].[SecurityPrinciple] P
		On	L.[PrincipleLogin] = P.[PrincipleLogin]
		Left Join [AppSecurity].[SecurityMembership] M
		On	P.[PrincipleId] = M.[PrincipleId]
		Left Join [AppSecurity].[SecurityRole] R
		On	M.[RoleId] = R.[RoleId]
		Left Join [AppSecurity].[SecurityPermission] S
		On	M.[RoleId] = S.[RoleId] And
			S.[ObjectId] = @ObjectId
		Left Join [AppSecurity].[SecurityOwner] O
		On	O.[ObjectId] = @ObjectId
Group By L.[PrincipleLogin],
		P.[PrincipleId]
GO