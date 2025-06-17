CREATE PROCEDURE [AppLibrary].[procGetLibraryMember]
		@ModelId UniqueIdentifier = Null,
		@LibraryId UniqueIdentifier = Null
As
/* Description: Performs Get on LibraryMember.
*/
Select	M.[LibraryId],
		M.[MemberId],
		M.[MemberParentId],
		L.[AssemblyName],
		N.[MemberNameSpace],
		M.[MemberName],
		M.[MemberType],
		M.[MemberData]
From	[AppLibrary].[LibraryMember] M
		Inner Join [AppLibrary].[LibrarySource] L
		On	M.[LibraryId] = L.[LibraryId]
		Left Join [AppLibrary].[LibraryModel] A
		On	M.[LibraryId] = A.[LibraryId]
		Cross Apply [App_DataDictionary].[funcGetMemberName] (M.[MemberId]) N
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@LibraryId is Null or @LibraryId = M.[LibraryId])
Order By [MemberNameSpace]
GO
