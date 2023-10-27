CREATE TABLE [App_DataDictionary].[LibraryMember]
(	[LibraryId] UniqueIdentifier Not Null,
	[MemberId] UniqueIdentifier Not Null,
	[ParentMemberId] UniqueIdentifier Null,
	-- 1023 is believed to be the maximum length of any given component of a .Net NameSpace value.
	-- There is no actual total limit to the length at the coding level.
	-- Each component is put into a separate row and the data is organized into a hierarchy.
	-- Key size in TSQL is limited to 1700 characters. 128 characters is a compromise.
	[MemberName] [App_DataDictionary].[typeNameSpaceElement] Not Null,
	[MemberType] NVarChar(50) Null, -- .Net Documents produce a letter that is converted to a Member type. There may not be one.
	[MemberData] XML Null, -- Raw data as XML
	-- Keys
	CONSTRAINT [PK_LibraryMember] PRIMARY KEY CLUSTERED ([LibraryId] ASC, [MemberId] ASC),
	CONSTRAINT [FK_LibraryMemberSource] FOREIGN KEY ([LibraryId]) REFERENCES [App_DataDictionary].[LibrarySource] ([LibraryId]),
	CONSTRAINT [FK_LibraryMemberParent] FOREIGN KEY ([LibraryId], [ParentMemberId]) REFERENCES [App_DataDictionary].[LibraryMember] ([LibraryId], [MemberId]),
)
GO
