CREATE TABLE [App_DataDictionary].[LibrarySource]
(
	-- Proof of Concept code.
	-- A Library is a generic structure based on the .Net Documentation Specification.
	-- There is no reason that other system cannot be stored provided it can be mangled into the same basic layout.
	-- The thought is that other external tools can produce an Spreadsheet style format and that can be parsed into this layout as well.
	-- https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
	-- https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/
	[SourceId] UniqueIdentifier Not Null CONSTRAINT [DF_LibrarySourceId] DEFAULT (newid()),
	[SourceTitle] [App_DataDictionary].[typeTitle] Not Null,
	[SourceDescription] [App_DataDictionary].[typeDescription] Null,
	[AssemblyName] NVarChar(1023) Not Null, -- Natural Key?
	[SourceDate] DateTime Not Null,
	[IsCodeSourced] Bit Not Null, -- Used to flag the library as something generated and should not be hand manipulated.
	-- Keys
	CONSTRAINT [PK_LibrarySource] PRIMARY KEY CLUSTERED ([SourceId] ASC),
)
GO
