CREATE PROCEDURE [App_DataDictionary].[procGetLibraryMember]
		@ModelId UniqueIdentifier = Null,
		@LibraryId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on LibraryMember.
*/
Declare @Data Table ( -- For performance
		[LibraryId] uniqueidentifier Not Null,
		[MemberId] uniqueidentifier Not Null,
		[MemberParentId] uniqueidentifier Null,
		[MemberName] [App_DataDictionary].[typeNameSpaceMember] NOT NULL,
		[NameSpace] NVarChar(Max) Null,
		Primary Key ([MemberId]))

;With [Data] As (
	Select	D.[LibraryId],
			[MemberId],
			[MemberParentId],
			[MemberName],
			Convert(NVarChar(Max), Null) As [NameSpace]
	From	[App_DataDictionary].[LibraryMember] D
			Left Join [App_DataDictionary].[ModelLibrary] A
				On	D.[LibraryId] = A.[LibraryId]
	Where	[MemberParentId] is Null And
			(@ModelId is Null or @ModelId = A.[ModelId]) And
			(@LibraryId is Null or @LibraryId = D.[LibraryId])
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
			On	P.[MemberId] = C.[MemberParentId])
Insert Into @Data
Select	[LibraryId],
		[MemberId],
		[MemberParentId],
		[MemberName],
		[NameSpace]
From	[Data]

Select	D.[LibraryId],
		D.[MemberId],
		D.[MemberParentId],
		L.[AssemblyName],
		B.[NameSpace],
		D.[MemberName],
		C.[ScopeName],
		D.[MemberType],
		D.[MemberData]
From	@Data B
		Inner Join [App_DataDictionary].[LibraryMember] D
		On	B.[MemberId] = D.[MemberId]
		Inner Join [App_DataDictionary].[LibrarySource] L
		On	D.[LibraryId] = L.[LibraryId]
		Left Join [App_DataDictionary].[ModelLibrary] A
		On	D.[LibraryId] = A.[LibraryId]
		Outer Apply [App_DataDictionary].[funcGetScopeName](D.[ScopeId]) C
Order By B.[NameSpace]
GO
