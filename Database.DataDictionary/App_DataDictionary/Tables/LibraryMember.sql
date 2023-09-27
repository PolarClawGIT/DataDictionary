CREATE TABLE [App_DataDictionary].[LibraryMember]
(	-- Leaf Node to the [LibararyMemberName].
	[LibraryId] UniqueIdentifier Not Null,
	[NameSpaceId] Int Not Null,
	[MemberId] Int Not Null,
	-- 1023 is believed to be the maximum length of any given component of a .Net NameSpace value.
	-- There is no actual total limit to the length at the coding level.
	-- Each component is put into a separate row and the data is organized into a hierarchy.
	-- Key size in TSQL is limited to 1700 characters. 500 characters is a compromise.
	[MemberName] NVarChar(500) Not Null,
	[MemberType] NVarChar(100) Null, -- .Net Documents produce a letter that is converted to a Member type. There may not be one.
	[MemberData] XML Null, -- Raw data as XML
	-- Keys
	CONSTRAINT [PK_LibraryMember] PRIMARY KEY CLUSTERED ([LibraryId] ASC, [NameSpaceId] ASC, [MemberId] ASC),
	CONSTRAINT [FK_LibraryMemberSource] FOREIGN KEY ([LibraryId]) REFERENCES [App_DataDictionary].[LibrarySource] ([LibraryId]),
	CONSTRAINT [FK_LibraryMemberScope] FOREIGN KEY ([LibraryId], [NameSpaceId]) REFERENCES [App_DataDictionary].[LibraryNameSpace] ([LibraryId], [NameSpaceId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_LibraryMember]
    ON [App_DataDictionary].[LibraryMember]([LibraryId], [NameSpaceId], [MemberName]);
GO
