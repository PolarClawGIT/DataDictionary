CREATE FUNCTION [AppLibrary].[funcGetMemberName] (@MemberId UniqueIdentifier)
-- This takes the Library Member Elements and rebuilds them into a fully Qualified Member NameSpace.
-- NameSpace Name is qualified by square brackets and delimited by periods.
-- This is based on the SQL Qualified Naming of objects.
RETURNS TABLE AS RETURN (
	With [Data] As (
		Select	[MemberId],
				NullIf([MemberParentId], [MemberId]) As [MemberParentId],
				[AppGeneral].[funcCreatePath]([MemberName], Null) As [MemberNameSpace],
				[AppGeneral].[funcCreatePath](Null, Null) As [ParentNameSpace],
				[MemberName]
		From	[AppLibrary].[LibraryMember]
		Where	[MemberId] = @MemberId
		Union All
		Select	D.[MemberId],
				NullIf(P.[MemberParentId], D.[MemberId]) As [MemberParentId],
				[AppGeneral].[funcCreatePath](P.[MemberName], D.[MemberNameSpace]) As [MemberNameSpace],
				[AppGeneral].[funcCreatePath](P.[MemberName], D.[ParentNameSpace]) As [ParentNameSpace],
				D.[MemberName]
		From	[Data] D
				Inner Join [AppLibrary].[LibraryMember] P
				On	D.[MemberParentId] = P.[MemberId])
	Select	[MemberId],
			[MemberNameSpace],
			[ParentNameSpace],
			[MemberName]
	From	[Data]
	Where	[MemberParentId] is Null)
GO