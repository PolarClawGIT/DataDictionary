-- Remember: Resource files do not update until they are saved directly.
Select	@@ServerName As [ServerName],
		D.[name] As [DatabaseName],
		D.[create_date] As [CreateDate],
		P.[name] As [Owner]
From	[sys].[databases] D
		Left Join [sys].[server_principals] P
		On D.[owner_sid] = P.[sid]
Where	db_name() = D.[name]