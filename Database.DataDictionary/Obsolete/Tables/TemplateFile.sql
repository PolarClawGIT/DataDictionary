CREATE TABLE [Obsolete].[TemplateFile]
(	-- This is the rules for how to construct a File Name out of an Object Name.
	-- This is a repeating group with only two possiblities.
	-- Input (XML), Process/Transform (XSLT), Ouput (Text or XML)
	-- This corisponds to the DocumentFile table.
	--
	-- Concept: The database would not store the files.
	--          Instead, they are generated and written to the Visual Studio solution directory.
	--          The Solution directory is typically in the Source control folder. example: C:\Users\username\source\repos
	--          The other place it can commonly fall is in My Documents directory.
	--          External tools then can better handle the display of the files.
	--          For this reason, the Directory is split into components.
	--          Root: is what special directory the solution is within. \\Source\repo, \\Source\Workspaces or \\Documents
	--          Solution: relative path from the root to the Visual Studio Solution directory
	--          Document: relative path from the solution to the directory to store the XML source documents
	--          Script: relative path from the solution to the directory to store the Scripting documents
	--          Prefix/Suffix: Added to the Element Name when BreakOnScope is used or concatenated.  Possible duplicate file names?
	--          Extension: The extension to be used. The Default is either .XML or .TXT depending on TransformOnText.
	--          A simple viewer would exist within the application to preview the data with "Save As" feature.
	--          The application can search for files based on the Template settings and display them in the navigation tree.
	[TemplateId]            UniqueIdentifier Not Null,
	[RelativePath]			[AppGeneral].[uddtFilePath] Null,
	[FileObject]			[AppGeneral].[uddtFileName] Null, -- Null, use the Object name provided otherwise override and use a fixed name.
	[FilePrefix]			[AppGeneral].[uddtFileAffix] Null,
	[FileSuffix]			[AppGeneral].[uddtFileAffix] Null,
	[FileExtension]			[AppGeneral].[uddtFileExtension] Null,
	--[FileType]				As (Convert(TinyInt,[IsInput]) | Convert(TinyInt,[IsProcess]) *2 | Convert(TinyInt,[IsOutput]) *4),
	[IsInput]				Bit Not Null CONSTRAINT [DF_TemplateFileInput] DEFAULT (0), -- File Type, Normally XML
	[IsProcess]				Bit Not Null CONSTRAINT [DF_TemplateFileProcess] DEFAULT (0), -- File Type, Normally XSLT
	[IsOutput]				Bit Not Null CONSTRAINT [DF_TemplateFileOutput] DEFAULT (0), -- File Type, Normally Text
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateFile_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateFile_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_TemplateFile] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [IsInput] ASC, [IsProcess] ASC, [IsOutput] ASC),
	CONSTRAINT [FK_Template] FOREIGN KEY ([TemplateId]) REFERENCES [Obsolete].[Template] ([TemplateId]),
	CONSTRAINT [CK_TemplateFileType] CHECK (([IsInput] = 1 And [IsProcess] = 0 And [IsOutput] = 0) Or ([IsInput] = 0 And [IsProcess] = 1 And [IsOutput] = 0) Or ([IsInput] = 0 And [IsProcess] = 0 And [IsOutput] = 1)),
)

