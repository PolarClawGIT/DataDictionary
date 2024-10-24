CREATE PROCEDURE [AppSecurity].[procGetSecurable]
		@SecurableId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Securable (Security Object).
*/
Select	T.[SecurableId],
		T.[SecurableTitle],
		T.[IsOrphaned],
		Convert(Bit, IIF(
				S.[IsSecurityAdmin] = 1 Or
				S.[IsOwner] = 1, 1, 0)) As [AlterValue],
		Convert(Bit, IIF(
				S.[IsSecurityAdmin] = 1 Or
				S.[IsOwner] = 1 ,1,0))
				As [AlterSecurity]
From	[AppSecurity].[Securable] T
		Cross Apply [AppSecurity].[funcAuthorization](T.[SecurableId]) S
Where	(@SecurableId is Null Or T.[SecurableId] = @SecurableId)
GO