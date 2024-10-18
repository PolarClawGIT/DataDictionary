CREATE FUNCTION [AppSecurity].[funcAuthorization] (@ObjectId UniqueIdentifier = null)
Returns Table With SchemaBinding
As Return
-- Master Security Function.
-- All other Security Functions use this function.
With [Login] As (
	-- Database Level security
	Select	Original_Login() As [PrincipalLogin],
			Convert(Bit, IIF(
				Is_RoleMember('DataDictionaryApp') = 0 And -- Cannot be executing using the application
				Is_RoleMember('db_denydatawriter') = 0 And
				(Is_RoleMember('db_datawriter') = 1 Or
				 Is_RoleMember('db_owner') = 1), 1, 0)) As [IsDbWriter])
Select	L.[PrincipalLogin],
		P.[PrincipalId],
		-- DB Security
		Convert(Bit,Min(Convert(Int,L.[IsDbWriter]))) As [IsDbWriter],
		-- Role Security
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
		-- Object Security
		@ObjectId As [ObjectId],
		Convert(Bit,Max(IIF(O.[PrincipalId] = P.[PrincipalId],1,0))) As [IsOwner],
		Convert(Bit,Max(IIF(O.[ObjectId] is Null, 0, 1))) As [HasOwner],
		Convert(Bit,Max(Convert(Int,IsNull(S.[IsGrant],0)))) As [IsGrant],
		Convert(Bit,Max(Convert(Int,IsNull(S.[IsDeny],0)))) As [IsDeny]
From	[Login] L
		-- Application Level Security
		Left Join[AppSecurity].[Principal] P
		On	L.[PrincipalLogin] = P.[PrincipalLogin]
		Left Join [AppSecurity].[RoleMembership] M
		On	P.[PrincipalId] = M.[PrincipalId]
		Left Join [AppSecurity].[Role] R
		On	M.[RoleId] = R.[RoleId]
		Left Join [AppSecurity].[ObjectPermission] S
		On	M.[RoleId] = S.[RoleId] And
			S.[ObjectId] = @ObjectId
		Left Join [AppSecurity].[ObjectOwner] O
		On	O.[ObjectId] = @ObjectId
Group By L.[PrincipalLogin],
		P.[PrincipalId]
GO