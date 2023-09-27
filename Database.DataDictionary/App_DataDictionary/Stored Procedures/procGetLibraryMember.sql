CREATE PROCEDURE [App_DataDictionary].[procGetLibraryMember]
		@ModelId UniqueIdentifier = Null,
		@LibraryId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on LibraryMember.
*/
;With [Data] As (
	Select	[LibraryId],
			[NameSpaceId],
			[NameSpaceParentId],
			Convert(NVarChar(Max),[NameSpace]) As [MemberNameSpace],
			Convert(Int,1) As [Level]
	From	[App_DataDictionary].[LibraryNameSpace]
	Where	[NameSpaceParentId] is Null
	Union All
	Select	C.[LibraryId],
			C.[NameSpaceId],
			C.[NameSpaceParentId],
			Convert(NVarChar(Max),FormatMessage('%s.%s',P.[MemberNameSpace],C.[NameSpace])) As [MemberNameSpace],
			P.[Level] + 1 As [Level]
	From	[App_DataDictionary].[LibraryNameSpace] C
			Inner Join [Data] P
			On	C.[LibraryId] = P.[LibraryId] And
				C.[NameSpaceParentId] = P.[NameSpaceId])
Select	D.[LibraryId],
		L.[AssemblyName],
		B.[MemberNameSpace],
		D.[MemberName],
		D.[MemberType],
		D.[MemberData]
From	[App_DataDictionary].[LibraryMember] D
		Left Join [App_DataDictionary].[ModelLibrary] A
		On	D.[LibraryId] = A.[LibraryId]
		Inner Join [App_DataDictionary].[LibrarySource] L
		On	D.[LibraryId] = L.[LibraryId]
		Left Join [Data] B
		On	D.[LibraryId] = B.[LibraryId] And
			D.[NameSpaceId] = B.[NameSpaceId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@LibraryId is Null or @LibraryId = D.[LibraryId])
GO