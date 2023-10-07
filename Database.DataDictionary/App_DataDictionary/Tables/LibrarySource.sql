CREATE TABLE [App_DataDictionary].[LibrarySource]
(
	-- Proof of Concept code.
	-- A Library is a generic structure based on the .Net Documentation Specification.
	-- There is no reason that other system cannot be stored provided it can be mangled into the same basic layout.
	-- The thought is that other external tools can produce an Spreadsheet style format and that can be parsed into this layout as well.
	-- https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
	-- https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/
	[LibraryId] UniqueIdentifier Not Null CONSTRAINT [DF_LibrarySourceId] DEFAULT (newid()),
	[LibraryTitle] [App_DataDictionary].[typeTitle] Not Null,-- expected to be the same as [AssemblyName], but can be changed.
	[LibraryDescription] [App_DataDictionary].[typeDescription] Null,
	[AssemblyName] NVarChar(1023) Not Null, -- Natural Key
	[SourceFile] NVarChar(500) Not Null, 
	[SourceDate] DateTime2 (7) Not Null,
	-- TODO: Add System Version later once the schema is locked down. Not needed for Db Schema?
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_LibrarySource_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_LibrarySource_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_LibrarySource_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_LibrarySource] PRIMARY KEY CLUSTERED ([LibraryId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_LibrarySourceAssembly]
    ON [App_DataDictionary].[LibrarySource]([AssemblyName]);
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_LibrarySourceTitle]
    ON [App_DataDictionary].[LibrarySource]([LibraryTitle]);
GO