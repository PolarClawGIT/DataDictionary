CREATE PROCEDURE [AppSecurity].[procGetSecurityRole]
		@RoleId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on SecurityRole.
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
		R.[IsScriptOwner]
From	[AppSecurity].[SecurityRole] R
Where	(@RoleId is Null or R.[RoleId] = @RoleId)
GO