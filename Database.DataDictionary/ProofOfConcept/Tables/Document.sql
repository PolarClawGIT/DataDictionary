CREATE TABLE [ProofOfConcept].[Document]
(	-- Documents to appear in the UI.
	[DocumentId]            UniqueIdentifier Not Null CONSTRAINT [DF_DocumentId] DEFAULT (newid()),
	[DocumentTitle]			[AppGeneral].[uddtTitle] Not Null, -- Default to Object Name
	--[DocumentDescription]	[AppGeneral].[uddtDescription] Null,
	--[DocumentContent]		NVarChar(Max) Null,
	-- File Location
	[RootFolder]			[AppGeneral].[uddtFileRoot] Not Null, -- Name of the Special Folder used as the Root defined in the Application.
	[RelativePath]			[AppGeneral].[uddtFilePath] Null, 
	[FileName]				[AppGeneral].[uddtFileName] Not Null, -- Can be built from Data Object
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Document_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Document_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED ([DocumentId] ASC),

)
