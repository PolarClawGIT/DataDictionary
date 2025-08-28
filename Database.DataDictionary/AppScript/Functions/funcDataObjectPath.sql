CREATE FUNCTION [AppScript].[funcDataObjectPath](@DataNameId UniqueIdentifier)
-- This takes the DataNameId and rebuilds them into a Data Object NameSpace.
-- NameSpace is qualified by square brackets and delimited by periods.
-- Temporal Data NOT Supported
RETURNS [AppGeneral].[uddtNameSpacePath] as 
BEGIN
	Declare @Result [AppGeneral].[uddtNameSpacePath] = null

	;With [Data] As (
	Select	[DataNameId],
			[ParentNameId],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[DataMember])) As [ObjectPath]
	From	[AppScript].[DataObjectName]
	Where	[DataNameId] = @DataNameId
	Union All
	Select	D.[DataNameId],
			NullIf(P.[ParentNameId], D.[DataNameId]) As [ParentNameId],
			Convert(NVarChar(Max),
				FormatMessage('[%s].%s',P.[DataMember],D.[ObjectPath])) As [ObjectPath]
	From	[Data] D
			Inner Join [AppScript].[DataObjectName] P
			On	D.[ParentNameId] = P.[DataNameId])
Select	@Result = [ObjectPath]
From	[Data]
Where	[ParentNameId] is Null

Return	@Result
END
