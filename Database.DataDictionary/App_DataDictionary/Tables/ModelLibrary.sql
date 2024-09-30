CREATE TABLE [App_DataDictionary].[ModelLibrary]
(
	[ModelId] UniqueIdentifier NOT NULL,
	[LibraryId] UniqueIdentifier NOT NULL,
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_ModelLibrary_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelLibrary_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelLibrary_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelLibrary] PRIMARY KEY CLUSTERED ([ModelId] ASC, [LibraryId] ASC),
	CONSTRAINT [FK_ModelLibraryModel] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelLibrarySource] FOREIGN KEY ([LibraryId]) REFERENCES [App_DataDictionary].[LibrarySource] ([LibraryId]),

)
