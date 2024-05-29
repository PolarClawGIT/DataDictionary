CREATE TABLE [App_DataDictionary].[ModelSubjectArea]
(
	[SubjectAreaId]          UniqueIdentifier NOT NULL CONSTRAINT [DF_ModelSubjectAreaId] DEFAULT (newsequentialid()),
	[SubjectAreaTitle]       [App_DataDictionary].[typeTitle] NULL,
	[SubjectAreaDescription] [App_DataDictionary].[typeDescription] NULL,
	[ModelId]                UniqueIdentifier NOT NULL,
	[NameSpaceId]            UniqueIdentifier NULL,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName NOT NULL CONSTRAINT [DF_ModelSubjectArea_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelSubjectArea_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelSubjectArea_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelSubjectArea] PRIMARY KEY CLUSTERED ([SubjectAreaId] ASC),
	CONSTRAINT [FK_ModelSubjectModel] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelSubjectNameSpace] FOREIGN KEY ([ModelId], [NameSpaceId]) REFERENCES [App_DataDictionary].[ModelNameSpace] ([ModelId], [NameSpaceId]),
	CONSTRAINT [UK_ModelSubjectArea] UNIQUE ([ModelId] ASC, [SubjectAreaId] ASC), -- FK's can use this.
)
GO
CREATE UNIQUE INDEX [AK_ModelSubjectArea]
    ON [App_DataDictionary].[ModelSubjectArea]([ModelId] ASC, [SubjectAreaTitle] ASC)
GO
CREATE UNIQUE INDEX [FK_ModelSubjectNameSpace] -- Also an AK
    ON [App_DataDictionary].[ModelSubjectArea]([ModelId] ASC, [NameSpaceId] ASC)
GO
