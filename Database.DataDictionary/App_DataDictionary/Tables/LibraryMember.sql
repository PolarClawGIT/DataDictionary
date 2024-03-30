CREATE TABLE [App_DataDictionary].[LibraryMember]
(	[MemberId]              UniqueIdentifier Not Null CONSTRAINT [DF_LibraryMemberId] DEFAULT (newid()),
	[LibraryId]             UniqueIdentifier Not Null,
	[MemberParentId]        UniqueIdentifier Null,
	[MemberName]            [App_DataDictionary].[typeNameSpaceMember] Not Null,
	[ScopeId]               Int Not Null,
	[MemberType]            NVarChar(10) Null, -- .Net Documents produce a letter that is converted to a Member type. There may not be one.
	[MemberData]            XML Null, -- Raw data as XML
	-- Keys
	CONSTRAINT [PK_LibraryMember] PRIMARY KEY CLUSTERED ([MemberId] ASC),
	CONSTRAINT [FK_LibraryMemberSource] FOREIGN KEY ([LibraryId]) REFERENCES [App_DataDictionary].[LibrarySource] ([LibraryId]),
	CONSTRAINT [FK_LibraryMemberParent] FOREIGN KEY ([MemberParentId]) REFERENCES [App_DataDictionary].[LibraryMember] ([MemberId]),
	CONSTRAINT [FK_LibraryMemberScope] FOREIGN KEY ([ScopeId]) REFERENCES [App_DataDictionary].[ApplicationScope] ([ScopeId]),
)
GO
CREATE NONCLUSTERED INDEX [FK_LibraryMemberParent]
    ON [App_DataDictionary].[LibraryMember]([MemberParentId]);
GO