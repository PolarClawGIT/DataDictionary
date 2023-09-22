CREATE TABLE [App_DataDictionary].[LibraryMember]
(
	[SourceId] UniqueIdentifier Not Null,
	[MemberId] Int Not Null, -- Maintained by Database. Application is not expected to know this.
	[MemberParentId] Int Null, -- Maintained by Database. Application is not expected to know this.
	-- 1023 is believed to be the maximum length of any given component of a .Net NameSpace value.
	-- There is no actual total limit to the length at the coding level.
	-- Each component is put into a separate row and the data is organized into a hierarchy.
	[MemberNameSpace] NVarChar(1023) Not Null,
	[MemberType] NVarChar(256) Null, -- .Net Documents produce a letter that is converted to a Member type. There may not be one.
	-- Keys
	CONSTRAINT [PK_LibraryMember] PRIMARY KEY CLUSTERED ([SourceId] ASC, [MemberId] ASC),
	CONSTRAINT [FK_LibraryMemberParent] FOREIGN KEY ([SourceId], [MemberParentId]) REFERENCES [App_DataDictionary].[LibraryMember] ([SourceId], [MemberId])
)
GO
--- Max Key Length is 1700 bytes. This Key is 2066 bytes. This cannot be enforced at the database level
/*
CREATE UNIQUE NONCLUSTERED INDEX [UX_LibraryMember]
    ON [App_DataDictionary].[LibraryMember]([SourceId], [MemberParentId], [MemberNameSpace]);
GO
*/