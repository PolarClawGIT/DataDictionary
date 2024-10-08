CREATE PROCEDURE [AppSecurity].[procGetSecurityPrinciple]
		@PrincipleId UniqueIdentifier = Null,
		@PrincipleLogin SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on SecurityPrinciple.
*/
;With [Logins] As (
	-- List of User known to the database.
	-- This does not include Users of groups managed outside of the database.
	-- The application does not support groups as application principals.
	-- Query to System Views are filtered by Database Security.
	Select	[name] As [PrincipleLogin]
	From	[sys].[database_principals]
	Where	[authentication_type] <> 0 And
			[name] Not In ('dbo','sys','INFORMATION_SCHEMA','guest')
	Union
	-- List of User known to the Application
	Select	[PrincipleLogin]
	From	[AppSecurity].[SecurityPrinciple])
Select	P.[PrincipleId],
		L.[PrincipleLogin],
		P.[PrincipleName],
		P.[PrincipleAnnotation],
		D.[type_desc] As [PrincipalType],
		Convert(Bit, IIF(D.[IsDbOwner] = 1 Or (D.[IsDataWriter] = 1 And D.[IsDenyDataWriter] = 0),1,0)) As [IsDbWriter],
		Convert(Bit, IIF(P.[PrincipleId] is Null,0,1)) As [IsApplicationUser]
From	[Logins] L
		Left Join [AppSecurity].[SecurityPrinciple] P
		On	L.[PrincipleLogin] = P.[PrincipleLogin]
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
			Where	P.[name] = L.[PrincipleLogin]
			Group By P.[name], P.[type_desc]) D
Where	(@PrincipleId is Null or P.[PrincipleId] = @PrincipleId) And
		(@PrincipleLogin is Null or L.[PrincipleLogin] = @PrincipleLogin)
GO