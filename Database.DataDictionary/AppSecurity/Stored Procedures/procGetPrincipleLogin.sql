CREATE PROCEDURE [AppSecurity].[procGetPrincipleLogin]
		@PrincipleLogin SysName = Null,
		@IsCurrent Bit = Null -- Returns only row for the Current/Original Login
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on PrincipleLogin.
** Because this procedure uses System Security Views, not all Logins may be included.
** This will return the list of Logins the current user is authorized to view, by SQL Security.
** Users that get permission to the database using a Windows group (or other authentication group)
** also do not appear in this list.
*/
Select	D.[name] As [PrincipleLogin],
		D.[type_desc] As [PrincipalType],
		Convert(Bit, IIF(S.[IsDbOwner] = 1 Or (S.[IsDataWriter] = 1 And S.[IsDenyDataWriter] = 0),1,0)) As [IsDbWriter],
		Convert(Bit, IIF(P.[PrincipleId] is Null,0,1)) As [IsApplicationUser]
From	[sys].[database_principals] D
		Left Join [AppSecurity].[SecurityPrinciple] P
		On	D.[name] = P.[PrincipleLogin]
		Outer Apply (
			-- Database Level Permissions.
			Select	P.[type_desc],
					Max(IIF(R.[name] in ('db_owner'),1,0)) As [IsDbOwner],
					Max(IIF(R.[name] in ('db_datawriter'),1,0)) As [IsDataWriter],
					Max(IIF(R.[name] in ('db_denydatawriter'),1,0)) As [IsDenyDataWriter]
			From	[sys].[database_principals] P
					Left Join [sys].[database_role_members] M
					On	P.[principal_id] = M.[member_principal_id]
					Left Join [sys].[database_principals] R
					On	M.[role_principal_id] = R.[principal_id]
			Where	P.[name] = D.[name]
			Group By P.[name], P.[type_desc]) S
Where	D.[authentication_type] <> 0 And
		D.[name] Not In ('dbo','sys','INFORMATION_SCHEMA','guest') And
		(@PrincipleLogin is Null or D.[name] = @PrincipleLogin) And
		(@IsCurrent = 1 And D.[name] = original_login())
GO