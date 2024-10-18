CREATE PROCEDURE [AppSecurity].[procGetAuthorization]
		@PrincipalLogin SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Authorization.
** Because this procedure uses System Security Views, not all Logins may be included.
** This will return the list of Logins the current user is authorized to view, by SQL Security.
** Users that get permission to the database using a Windows group (or other authentication group)
** also do not appear in this list.
**
** The current Login is always returned.
*/
;With [Logins] As (
	Select	[name] As [PrincipalLogin]
	From	[sys].[database_principals]
	Where	[name] Not In ('dbo','sys','INFORMATION_SCHEMA','guest') And
			[authentication_type] <> 0
	Union
	Select	Original_Login() As [PrincipalLogin])
Select	L.[PrincipalLogin], -- Will not be Null, PK
		-- Principal Data, may be null if not an Application User
		P.[PrincipalId],
		P.[PrincipalName],
		-- Role Security
		S.[IsSecurityAdmin],
		S.[IsHelpAdmin],
		S.[IsHelpOwner],
		S.[IsCatalogAdmin],
		S.[IsCatalogOwner],
		S.[IsLibraryAdmin],
		S.[IsLibraryOwner],
		S.[IsModelAdmin],
		S.[IsModelOwner],
		S.[IsScriptAdmin],
		S.[IsScriptOwner]
From	[Logins] L
		-- Only returns values if the Login is also a Application User
		Left Join [AppSecurity].[Principal] P
		On	L.[PrincipalLogin] = P.[PrincipalLogin]
		-- Only Returns values for the original_login. Everything else gets Null.
		Left Join [AppSecurity].[funcAuthorization](null) S
		On	L.[PrincipalLogin] = S.[PrincipalLogin]
Where	(@PrincipalLogin is Null or L.[PrincipalLogin] = @PrincipalLogin)
GO