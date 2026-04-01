CREATE PROCEDURE [AppSecurity].[procGetAuthorizationSecurable]
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Authorization Securable.
**
** Only returns rows for the Current User
*/
Select	[PrincipalId],
		[SecurableId],
		[SecurableTitle],
		[IsOwner],
		[IsGrant],
		[IsDeny]
From	[AppSecurity].[Securable] O
		Cross Apply [AppSecurity].[funcAuthorization](O.[SecurableId]) S
Print FormatMessage ('Select: %i, %s', @@RowCount, Convert(VarChar,GetDate()));
Go