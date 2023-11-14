CREATE TABLE [App_DataDictionary].[LibraryMember]
(	[MemberId] UniqueIdentifier Not Null CONSTRAINT [DF_LibraryMemberId] DEFAULT (newid()),
	[LibraryId] UniqueIdentifier Not Null,
	[MemberParentId] UniqueIdentifier Null,
	[MemberName] [App_DataDictionary].[typeAliasElement] Not Null,
	[MemberType] NVarChar(50) Null, -- .Net Documents produce a letter that is converted to a Member type. There may not be one.
	[MemberData] XML Null, -- Raw data as XML
	-- Keys
	CONSTRAINT [PK_LibraryMember] PRIMARY KEY CLUSTERED ([LibraryId] ASC, [MemberId] ASC),
	CONSTRAINT [FK_LibraryMemberSource] FOREIGN KEY ([LibraryId]) REFERENCES [App_DataDictionary].[LibrarySource] ([LibraryId]),
	CONSTRAINT [FK_LibraryMemberParent] FOREIGN KEY ([LibraryId], [MemberParentId]) REFERENCES [App_DataDictionary].[LibraryMember] ([LibraryId], [MemberId]),
)
GO
