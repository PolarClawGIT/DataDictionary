CREATE PROCEDURE [AppSecurity].[procGetPrincipal]
		@PrincipalId UniqueIdentifier = Null,
		@PrincipalLogin SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Security Principal.
*/
Select	P.[PrincipalId],
		P.[PrincipalLogin],
		P.[PrincipalName],
		P.[PrincipalAnnotation],
		S.[IsSecurityAdmin] As [AlterValue],
		S.[IsSecurityAdmin] As [AlterSecurity]
From	[AppSecurity].[Principal] P
		Cross Apply [AppSecurity].[funcAuthorization](null) S
Where	(@PrincipalId is Null or P.[PrincipalId] = @PrincipalId) And
		(@PrincipalLogin is Null or P.[PrincipalLogin] = @PrincipalLogin)
GO