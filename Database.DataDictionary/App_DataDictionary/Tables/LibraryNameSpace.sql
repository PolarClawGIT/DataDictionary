CREATE TABLE [App_DataDictionary].[LibraryNameSpace]
(	-- Creates a hierarchy of for the naming structure composed of the NameSpace components.
	-- NameSpace may have different meanings depending on language. This assumes ASP.Net naming.
	[LibraryId] UniqueIdentifier Not Null,
	[NameSpaceId] Int Not Null,
	[NameSpaceParentId] Int Null,
	-- 1023 is believed to be the maximum length of any given component of a .Net NameSpace value.
	-- There is no actual total limit to the length at the coding level.
	-- Each component is put into a separate row and the data is organized into a hierarchy.
	-- Key size in TSQL is limited to 1700 characters. 500 characters is a compromise.
	[NameSpace] NVarChar(500) Not Null,
	CONSTRAINT [PK_LibraryNameSpace] PRIMARY KEY CLUSTERED ([LibraryId] ASC, [NameSpaceId] ASC),
	CONSTRAINT [FK_LibraryNameSpaceParent] FOREIGN KEY ([LibraryId], [NameSpaceParentId]) REFERENCES [App_DataDictionary].[LibraryNameSpace] ([LibraryId], [NameSpaceId])
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_LibraryNameSpace]
    ON [App_DataDictionary].[LibraryNameSpace]([LibraryId], [NameSpaceParentId], [NameSpace]);
GO
