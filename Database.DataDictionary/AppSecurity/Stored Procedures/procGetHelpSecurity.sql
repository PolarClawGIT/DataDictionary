CREATE PROCEDURE [AppSecurity].[procGetHelpSecurity]
		@HelpId UniqueIdentifier = Null,
		@RoleId UniqueIdentifier = Null,
		@PrincipleId UniqueIdentifier = Null
AS
Select	T.[HelpId],
		O.[PrincipleId] As [PrincipleId],
		Convert(UniqueIdentifier, Null) [RoleId],
		Convert(Bit, 1) As [IsGrant],
		Convert(Bit, 0) As [IsDeny]
From	[AppGeneral].[HelpSubject] T
		Left Join [AppSecurity].[SecurityOwner] O
		On	T.[HelpId] = O.[ObjectId]
Where	(@HelpId is Null Or T.[HelpId] = @HelpId) And
		(@PrincipleId is Null Or IsNull(O.[PrincipleId], @PrincipleId) = @PrincipleId)
Union
Select	T.[HelpId],
		Convert(UniqueIdentifier, Null) As [PrincipleId],
		P.[RoleId],
		P.[IsGrant],
		P.[IsDeny]
From	[AppGeneral].[HelpSubject] T
		Left Join [AppSecurity].[SecurityPermission] P
		On	T.[HelpId] = P.[ObjectId]
Where	(@HelpId is Null Or T.[HelpId] = @HelpId) And
		(@RoleId is Null Or IsNull(P.[RoleId], @RoleId) = @RoleId)
GO
