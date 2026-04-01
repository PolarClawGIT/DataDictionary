CREATE TABLE [Obsolete].[DocumentFile]
(	-- This is a repeating group with only three possiblities.
	-- Input (XML), Process/Transform (XSLT), Ouput (Text or XML)
	[DocumentId]            UniqueIdentifier NOT Null,
	[RelativePath]			[AppGeneral].[uddtFilePath] Null, 
	[FileName]				[AppGeneral].[uddtFileName] Not Null, 
	--[IsXml]					Bit Not Null CONSTRAINT [DF_DocumentFileXML] DEFAULT (0), 
	--[FileType]				As (Convert(TinyInt,[IsInput]) | Convert(TinyInt,[IsProcess]) *2 | Convert(TinyInt,[IsOutput]) *4),
	[IsInput]				Bit Not Null CONSTRAINT [DF_DocumentFileInput] DEFAULT (0), -- File Type, Normally XML
	[IsProcess]				Bit Not Null CONSTRAINT [DF_DocumentFileProcess] DEFAULT (0), -- File Type, Normally XSLT
	[IsOutput]				Bit Not Null CONSTRAINT [DF_DocumentFileOutput] DEFAULT (0), -- File Type, Normally Text
	--[FileContent]			NVarChar(Max) Null, -- Content is stored on the File system. May be stored in Db later?
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DocumentFile_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DocumentFile_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_DocumentFile] PRIMARY KEY CLUSTERED ([DocumentId] ASC, [IsInput] ASC, [IsProcess] ASC, [IsOutput] ASC),
	CONSTRAINT [FK_Document] FOREIGN KEY ([DocumentId]) REFERENCES [Obsolete].[Document] ([DocumentId]),
	CONSTRAINT [CK_DocumentFileType] CHECK (([IsInput] = 1 And [IsProcess] = 0 And [IsOutput] = 0) Or ([IsInput] = 0 And [IsProcess] = 1 And [IsOutput] = 0) Or ([IsInput] = 0 And [IsProcess] = 0 And [IsOutput] = 1)),
)
