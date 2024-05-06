CREATE PROCEDURE [App_DataDictionary].[procGetLibraryMember]
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
From	[App_DataDictionary].[LibraryMember] M
		Inner Join [App_DataDictionary].[LibrarySource] L
		On	M.[LibraryId] = L.[LibraryId]
		Left Join [App_DataDictionary].[ModelLibrary] A
		On	M.[LibraryId] = A.[LibraryId]
		Cross Apply [App_DataDictionary].[funcGetMemberName] (M.[MemberId]) N
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@LibraryId is Null or @LibraryId = M.[LibraryId])
Order By [MemberNameSpace]
GO
