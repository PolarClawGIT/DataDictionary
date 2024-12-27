CREATE FUNCTION [AppSecurity].[funcApplicationAuthorization] ()
Returns Table With SchemaBinding as Return
-- Row Level Security
-- Only the Application, db_datawriter, or db_owner can modify the rows.
-- The Application uses stored procedures and does further checks to verify Authorization.
-- One (granted) or zero (denied) rows are returned.
With [Authorization] As (
	Select	Original_Login() As [PrincipalLogin],
			Is_RoleMember('DataDictionaryApp') As [IsApplication],
			IIF(
				Is_RoleMember('DataDictionaryApp') = 0 And -- Cannot be executing using the application
				Is_RoleMember('db_denydatawriter') = 0 And
				(Is_RoleMember('db_datawriter') = 1 Or
				 Is_RoleMember('db_owner') = 1), 1, 0) As [IsDbWriter])
Select	[PrincipalLogin],
		[IsApplication],
		[IsDbWriter]
From	[Authorization]
Where	[IsApplication] = 1 Or
		[IsDbWriter] = 1
GO