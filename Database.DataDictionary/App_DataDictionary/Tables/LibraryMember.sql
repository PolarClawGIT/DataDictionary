CREATE TABLE [App_DataDictionary].[LibraryMember]
(	[MemberId]              UniqueIdentifier Not Null CONSTRAINT [DF_LibraryMemberId] DEFAULT (newid()),
	[LibraryId]             UniqueIdentifier Not Null,
	[MemberParentId]        UniqueIdentifier Null,
	[MemberName]            [App_DataDictionary].[typeNameSpaceMember] Not Null,
	[MemberType]            [App_DataDictionary].[typeObjectSubType] Null, -- .Net Documents produce a letter that is converted to a Member type. There may not be one.
	[MemberData]            XML Null, -- Raw data as XML
	-- Keys
	CONSTRAINT [PK_LibraryMember] PRIMARY KEY CLUSTERED ([MemberId] ASC),
	CONSTRAINT [FK_LibraryMemberSource] FOREIGN KEY ([LibraryId]) REFERENCES [App_DataDictionary].[LibrarySource] ([LibraryId]),
	CONSTRAINT [FK_LibraryMemberParent] FOREIGN KEY ([MemberParentId]) REFERENCES [App_DataDictionary].[LibraryMember] ([MemberId]),
	CONSTRAINT [CK_LibraryMemberType] CHECK ([MemberType]='Parameter' OR [MemberType]='Event' OR [MemberType]='Method' OR [MemberType]='Property' OR [MemberType]='Field' OR [MemberType]='Type' OR [MemberType]='NameSpace' OR [MemberType] IS NULL),
)
GO
CREATE NONCLUSTERED INDEX [FK_LibraryMemberParent]
    ON [App_DataDictionary].[LibraryMember]([MemberParentId]);
GO