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
		T.[SecurableTitle]
From	[AppSecurity].[SecurableOwner] O
		Inner Join [AppSecurity].[Securable] T
		On	O.[SecurableId] = T.[SecurableId]
Where	(@SecurableId is Null Or O.[SecurableId] = @SecurableId) And
		(@PrincipalId is Null Or O.[PrincipalId] = @PrincipalId)
GO