CREATE FUNCTION [AppScript].[funcDataItemPath](@DataItemId UniqueIdentifier)
-- This takes the DataItemId and rebuilds them into a Data Item NameSpace.
-- NameSpace is qualified by square brackets and delimited by periods.
-- Temporal Data NOT Supported
RETURNS [AppGeneral].[uddtNameSpacePath] as 
BEGIN
	Declare @Result [AppGeneral].[uddtNameSpacePath] = null

	;With [Data] As (
	Select	[DataItemId],
			NullIf([ParentItemId], [DataItemId]) As [ParentItemId],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[DataItemMember])) As [DataNameSpace]
	From	[AppScript].[DataItem]
	Where	[DataItemId] = @DataItemId
	Union All
	Select	D.[DataItemId],
			NullIf(P.[ParentItemId], D.[DataItemId]) As [ParentItemId],
			Convert(NVarChar(Max),
				FormatMessage('[%s].%s',P.[DataItemMember],D.[DataNameSpace])) As [DataNameSpace]
	From	[Data] D
			Inner Join [AppScript].[DataItem] P
			On	D.[ParentItemId] = P.[DataItemId])
Select	@Result = [DataNameSpace]
From	[Data]
Where	[ParentItemId] is Null

Return	@Result
END