CREATE TABLE [App_DataDictionary].[DomainSubjectArea]
(
	[SubjectAreaId] UniqueIdentifier Not Null CONSTRAINT [DF_ModelSubjectAreaId] DEFAULT (newsequentialid()),
	[SubjectAreaTitle] [App_DataDictionary].[typeTitle] Not Null,
	[SubjectAreaDescription] [App_DataDictionary].[typeDescription] Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_SubjectArea_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_SubjectArea_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_SubjectArea_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainSubjectArea] PRIMARY KEY CLUSTERED ([SubjectAreaId] ASC),
)
GO