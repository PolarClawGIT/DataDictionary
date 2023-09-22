CREATE TABLE [App_DataDictionary].[LibraryDocument]
(
	-- Also called tag. Different systems have different names.
	-- This is where the detail text appears.
	[SourceId] UniqueIdentifier Not Null,
	[MemberId] Int Not Null,
	[DocumentName]  NVarChar(256) Not Null, -- The length is based on the documentation from Microsoft shows the tags are short. Longer names are possible.
	[DocumentContents] NVarChar(Max) Null, -- .Net application produce XML. Other tools may use other formats. The Application handles this as text, regardless of actual content.
	-- Keys
	CONSTRAINT [PK_LibraryDocument] PRIMARY KEY CLUSTERED ([SourceId] ASC, [MemberId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_LibraryDocument]
    ON [App_DataDictionary].[LibraryDocument]([SourceId], [MemberId], [DocumentName]);
GO