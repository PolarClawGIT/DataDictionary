CREATE FUNCTION [App_DataDictionary].[funcGetMemberNameSpace] (@MemberId UniqueIdentifier)
-- This takes the Library Member Elements and rebuilds them into a fully Qualified Member NameSpace.
-- NameSpace Name is qualified by square brackets and delimited by periods.
-- This is based on the SQL Qualified Naming of objects.
RETURNS TABLE AS RETURN (
	With [Data] As (
		Select	[MemberId],
				NullIf([MemberParentId], [MemberId]) As [MemberParentId],
				Convert(NVarChar(Max),
					FormatMessage('[%s]',[MemberName])) As [MemberNameSpace],
				Convert(NVarChar(Max), Null) As [ParentNameSpace],
				[MemberName],
				[MemberType]
		From	[App_DataDictionary].[LibraryMember]
		Where	[MemberId] = @MemberId
		Union All
		Select	D.[MemberId],
				NullIf(P.[MemberParentId], D.[MemberId]) As [MemberParentId],
				Convert(NVarChar(Max),
					FormatMessage('[%s].%s',P.[MemberName],D.[MemberNameSpace])) As [MemberNameSpace],
				Convert(NVarChar(Max),
					IIF(D.[ParentNameSpace] is Null,
					FormatMessage('[%s]',P.[MemberName]),
					FormatMessage('[%s].%s',P.[MemberName], D.[ParentNameSpace])))
					As [ParentNameSpace],
				D.[MemberName],
				P.[MemberType]
		From	[Data] D
				Inner Join [App_DataDictionary].[LibraryMember] P
				On	D.[MemberParentId] = P.[MemberId])
	Select	[MemberId],
			[MemberNameSpace],
			[ParentNameSpace],
			[MemberName],
			[MemberType]
	From	[Data]
	Where	[MemberParentId] is Null)
GO