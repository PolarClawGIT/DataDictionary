CREATE PROCEDURE [AppSecurity].[procGetSecurableOwner]
		@SecurableId UniqueIdentifier = Null,
		@PrincipalId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Securable Owner (Principal).
*/
Select	O.[PrincipalId],
		O.[SecurableId],
		T.[SecurableTitle],
		Convert(Bit, IIF(
				S.[IsSecurityAdmin] = 1 Or
				S.[IsOwner] = 1 ,1,0))
				As [AlterValue],
		Convert(Bit, IIF(
				S.[IsSecurityAdmin] = 1 Or
				S.[IsOwner] = 1 ,1,0))
				As [AlterSecurity]
From	[AppSecurity].[SecurableOwner] O
		Inner Join [AppSecurity].[Securable] T
		On	O.[SecurableId] = T.[SecurableId]
		Cross Apply [AppSecurity].[funcAuthorization](O.[SecurableId]) S
Where	(@SecurableId is Null Or O.[SecurableId] = @SecurableId) And
		(@PrincipalId is Null Or O.[PrincipalId] = @PrincipalId)
GO