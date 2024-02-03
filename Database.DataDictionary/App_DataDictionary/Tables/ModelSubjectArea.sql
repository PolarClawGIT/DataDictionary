CREATE TABLE [App_DataDictionary].[ModelSubjectArea]
(
	[SubjectAreaId]          UniqueIdentifier NOT NULL CONSTRAINT [DF_ModelSubjectAreaId] DEFAULT (newsequentialid()),
	[ModelId]                UniqueIdentifier NOT NULL,
	--[SubjectAreaParentId]    UniqueIdentifier NULL,
	[SubjectAreaTitle]       [App_DataDictionary].[typeTitle] NOT NULL,
	[SubjectAreaDescription] [App_DataDictionary].[typeDescription] NULL,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName NOT NULL CONSTRAINT [DF_ModelSubjectArea_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelSubjectArea_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelSubjectArea_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelSubjectArea] PRIMARY KEY CLUSTERED ([SubjectAreaId] ASC),
	CONSTRAINT [UK_ModelSubjectArea] UNIQUE ([ModelId] ASC, [SubjectAreaId] ASC), -- FK's go to this.
	--CONSTRAINT [FK_ModelSubjectAreaParent] FOREIGN KEY ([ModelId], [SubjectAreaParentId]) REFERENCES [App_DataDictionary].[ModelSubjectArea] ([ModelId], [SubjectAreaId]),
)
GO
CREATE UNIQUE INDEX [AK_DomainSubjectArea]
    ON [App_DataDictionary].[ModelSubjectArea]([ModelId] ASC, [SubjectAreaTitle] ASC)
GO