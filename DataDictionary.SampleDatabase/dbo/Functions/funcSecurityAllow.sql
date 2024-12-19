CREATE FUNCTION [dbo].[funcSecurityAllow] ()
Returns Table With SchemaBinding
As Return 
Select	Convert(Bit, 1) As [IsAllowed]
Where	1=1
GO