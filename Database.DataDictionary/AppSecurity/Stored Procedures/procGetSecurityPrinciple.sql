CREATE PROCEDURE [AppSecurity].[procGetSecurityPrinciple]
		@PrincipleId UniqueIdentifier = Null,
		@PrincipleLogin SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on SecurityPrinciple.
*/
Select	P.[PrincipleId],
		P.[PrincipleLogin],
		P.[PrincipleName],
		P.[PrincipleAnnotation],
		S.[IsSecurityAdmin] As [AlterValue],
		S.[IsSecurityAdmin] As [AlterSecurity]
From	[AppSecurity].[SecurityPrinciple] P
		Cross Apply [AppSecurity].[funcSecurityPermisson](null) S
Where	(@PrincipleId is Null or P.[PrincipleId] = @PrincipleId) And
		(@PrincipleLogin is Null or P.[PrincipleLogin] = @PrincipleLogin)
GO