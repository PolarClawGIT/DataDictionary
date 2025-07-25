CREATE FUNCTION [AppScript].[funcDataObjectPath](@DataObjectId UniqueIdentifier)
-- This takes the DataItemId and rebuilds them into a Data Item NameSpace.
-- NameSpace is qualified by square brackets and delimited by periods.
-- Temporal Data NOT Supported
RETURNS [AppGeneral].[uddtNameSpacePath] as 
BEGIN
	Declare @Result [AppGeneral].[uddtNameSpacePath] = null

	;With [Data] As (
	Select	[DataObjectId],
			NullIf([ParentObjectId], [DataObjectId]) As [ParentObjectId],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[DataMember])) As [DataNameSpace]
	From	[AppScript].[DataObject]
	Where	[DataObjectId] = @DataObjectId
	Union All
	Select	D.[DataObjectId],
			NullIf(P.[ParentObjectId], D.[DataObjectId]) As [ParentObjectId],
			Convert(NVarChar(Max),
				FormatMessage('[%s].%s',P.[DataMember],D.[DataNameSpace])) As [DataNameSpace]
	From	[Data] D
			Inner Join [AppScript].[DataObject] P
			On	D.[ParentObjectId] = P.[DataObjectId])
Select	@Result = [DataNameSpace]
From	[Data]
Where	[ParentObjectId] is Null

Return	@Result
END