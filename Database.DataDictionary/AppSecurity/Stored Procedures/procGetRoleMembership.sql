CREATE PROCEDURE [AppSecurity].[procGetRoleMembership]
		@RoleId UniqueIdentifier = Null,
		@PrincipleId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Security Membership.
*/
Select	M.[RoleId],
		M.[PrincipleId],
		S.[IsSecurityAdmin] As [AlterValue],
		S.[IsSecurityAdmin] As [AlterSecurity]
From	[AppSecurity].[RoleMembership] M
		Cross Apply [AppSecurity].[funcAuthorization](null) S
Where	(@RoleId is Null or [RoleId] = @RoleId) And
		(@PrincipleId is Null or M.[PrincipleId] = @PrincipleId)
GO