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
			[MemberId],
			[MemberParentId],
			[MemberName],
			Convert(NVarChar(Max), Null) As [NameSpace]
	From	[App_DataDictionary].[LibraryMember]
	Where	[MemberParentId] is Null
	Union All
	Select	C.[LibraryId],
			C.[MemberId],
			C.[MemberParentId],
			C.[MemberName],
			IIf(P.[NameSpace] is Null,
				Convert(NVarChar(Max), P.[MemberName]),
				Convert(NVarChar(Max), FormatMessage('%s.%s', P.[NameSpace], P.[MemberName])))
				As [NameSpace]
	From	[Data] P
			Inner Join [App_DataDictionary].[LibraryMember] C
			On	P.[LibraryId] = C.[LibraryId] And
				P.[MemberId] = C.[MemberParentId])
Select	D.[LibraryId],
		D.[MemberId],
		D.[MemberParentId],
		L.[AssemblyName],
		B.[NameSpace],
		D.[MemberName],
		C.[ScopeName],
		D.[MemberType],
		D.[MemberData]
From	[App_DataDictionary].[LibraryMember] D
		Inner Join [App_DataDictionary].[LibrarySource] L
		On	D.[LibraryId] = L.[LibraryId]
		Left Join [Data] B
		On	D.[LibraryId] = B.[LibraryId] And
			D.[MemberId] = B.[MemberId]
		Left Join [App_DataDictionary].[ModelLibrary] A
		On	D.[LibraryId] = A.[LibraryId]
		Outer Apply [App_DataDictionary].[funcGetScopeName](D.[ScopeId]) C
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@LibraryId is Null or @LibraryId = D.[LibraryId])
Order By B.[NameSpace]
GO
