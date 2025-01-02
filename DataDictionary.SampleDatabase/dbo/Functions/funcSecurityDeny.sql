CREATE FUNCTION [dbo].[funcSecurityDeny] ()
Returns Table With SchemaBinding
As Return 
Select	Convert(Bit, 1) As [IsAllowed]
Where	1=2
GO