CREATE PROCEDURE [AppSecurity].[procGetRole]
		@RoleId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Security Role.
*/
Select	R.[RoleId],
		R.[RoleName],
		R.[RoleDescription],
		R.[IsSecurityAdmin],
		R.[IsHelpAdmin],
		R.[IsHelpOwner],
		R.[IsCatalogAdmin],
		R.[IsCatalogOwner],
		R.[IsLibraryAdmin],
		R.[IsLibraryOwner],
		R.[IsModelAdmin],
		R.[IsModelOwner],
		R.[IsScriptAdmin],
		R.[IsScriptOwner],
		S.[IsSecurityAdmin] As [AlterValue],
		S.[IsSecurityAdmin] As [AlterSecurity]
From	[AppSecurity].[Role] R
		Cross Apply [AppSecurity].[funcAuthorization](null) S
Where	(@RoleId is Null or R.[RoleId] = @RoleId)
GO