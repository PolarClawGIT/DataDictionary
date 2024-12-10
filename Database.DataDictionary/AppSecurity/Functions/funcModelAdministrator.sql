CREATE FUNCTION [AppSecurity].[funcModelAdministrator]()
Returns Table With SchemaBinding
As Return 
Select	Convert(Bit, 1) As [IsAllowed]
From	[AppSecurity].[funcAuthorization](Null) F
Where	[IsDbWriter] = 1 Or [IsModelAdmin] = 1
GO