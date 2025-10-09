CREATE FUNCTION [AppScript].[funcObjectPath](@ObjectNameId UniqueIdentifier)
-- This takes the DataNameId and rebuilds them into a Data Object NameSpace.
-- NameSpace is qualified by square brackets and delimited by periods.
-- Temporal Data NOT Supported
RETURNS [AppGeneral].[uddtNameSpacePath] as 
BEGIN
	Declare @Result [AppGeneral].[uddtNameSpacePath] = null

	;With [Data] As (
	Select	[ObjectNameId],
			[ParentNameId],
			[AppGeneral].[funcCreatePath]([ObjectMember], Null) As [ObjectPath]
	From	[AppScript].[DataObjectName]
	Where	[ObjectNameId] = @ObjectNameId
	Union All
	Select	D.[ObjectNameId],
			NullIf(P.[ParentNameId], D.[ObjectNameId]) As [ParentNameId],
			[AppGeneral].[funcCreatePath](P.[ObjectMember],D.[ObjectPath]) As [ObjectPath]
	From	[Data] D
			Inner Join [AppScript].[DataObjectName] P
			On	D.[ParentNameId] = P.[ObjectNameId])
Select	@Result = [ObjectPath]
From	[Data]
Where	[ParentNameId] is Null

Return	@Result
END
