CREATE TABLE [App_DataDictionary].[ModelSubjectArea]
(
	[SubjectAreaId] UniqueIdentifier Not Null CONSTRAINT [DF_ModelSubjectAreaId] DEFAULT (newsequentialid()),
	[ModelId] UniqueIdentifier NOT NULL,
	[SubjectAreaTitle] [App_DataDictionary].[typeTitle] Not Null,
	[SubjectAreaDescription] [App_DataDictionary].[typeDescription] Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ModelSubjectArea_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelSubjectArea_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelSubjectArea_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelSubjectArea] PRIMARY KEY CLUSTERED ([SubjectAreaId] ASC),
	CONSTRAINT [FK_ModelSubjectAreaModel] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ModelSubjectArea]
    ON [App_DataDictionary].[ModelSubjectArea]([ModelId] ASC, [SubjectAreaTitle] ASC);
GO