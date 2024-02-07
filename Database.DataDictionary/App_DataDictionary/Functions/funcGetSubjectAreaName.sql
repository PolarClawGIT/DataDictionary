CREATE FUNCTION [App_DataDictionary].[funcGetSubjectAreaName](@SubjectAreaId UniqueIdentifier)
-- This takes the Subject Area Members and rebuilds them into a fully Qualified Subject Area NameSpace.
-- Subject Area NameSpace is qualified by square brackets and delimited by periods.
-- This is based on the SQL Qualified Naming of objects.
RETURNS TABLE AS RETURN (
With [Data] As (
	Select	[SubjectAreaId],
			NullIf([SubjectAreaParentId], [SubjectAreaId]) As [SubjectAreaParentId],
			Convert(NVarChar(Max),
				FormatMessage('[%s]',[SubjectAreaMember])) As [SubjectAreaNameSpace],
			Convert(NVarChar(Max), Null) As [ParentSubjectAreaNameSpace],
			[SubjectAreaMember]
	From	[App_DataDictionary].[ModelSubjectArea]
	Where	[SubjectAreaId] = @SubjectAreaId
	Union All
	Select	D.[SubjectAreaId],
			NullIf(P.[SubjectAreaParentId], D.[SubjectAreaId]) As [SubjectAreaParentId],
			Convert(NVarChar(Max),
				FormatMessage('[%s].%s',P.[SubjectAreaMember],D.[SubjectAreaNameSpace])) As [SubjectAreaNameSpace],
			Convert(NVarChar(Max),
				IIF(D.[ParentSubjectAreaNameSpace] is Null,
				FormatMessage('[%s]',P.[SubjectAreaMember]),
				FormatMessage('[%s].%s',P.[SubjectAreaMember], D.[ParentSubjectAreaNameSpace])))
				As [ParentSubjectAreaNameSpace],
			D.[SubjectAreaMember]
	From	[Data] D
			Inner Join [App_DataDictionary].[ModelSubjectArea] P
			On	D.[SubjectAreaParentId] = P.[SubjectAreaId])
Select	[SubjectAreaId],
		[SubjectAreaNameSpace],
		[ParentSubjectAreaNameSpace],
		[SubjectAreaMember]
From	[Data]
Where	[SubjectAreaParentId] is Null)
GO