CREATE TABLE [App_DataDictionary].[ModelSubjectAttribute] (
    [ModelId]       UNIQUEIDENTIFIER NOT NULL,
    [SubjectAreaId] UNIQUEIDENTIFIER NOT NULL,
    [AttributeId]   UNIQUEIDENTIFIER NOT NULL,
    [NameSpaceId]   UNIQUEIDENTIFIER NOT NULL,
    [ModifiedBy]    SysName                                            CONSTRAINT [DF_ModelSubjectAttribute_ModifiedBy] DEFAULT (original_login()) NOT NULL,
    [SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ModelSubjectAttribute_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_ModelSubjectAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    CONSTRAINT [PK_ModelSubjectAttribute] PRIMARY KEY CLUSTERED ([ModelId] ASC, [SubjectAreaId] ASC, [AttributeId] ASC),
    CONSTRAINT [FK_ModelSubjectAttribute_Attribute] FOREIGN KEY ([ModelId], [AttributeId]) REFERENCES [App_DataDictionary].[ModelAttribute] ([ModelId], [AttributeId]),
    CONSTRAINT [FK_ModelSubjectAttribute_Subject] FOREIGN KEY ([ModelId], [SubjectAreaId]) REFERENCES [App_DataDictionary].[ModelSubjectArea] ([ModelId], [SubjectAreaId]),
    CONSTRAINT [FK_ModelSubjectAttribute_NameSpace] FOREIGN KEY ([NameSpaceId]) REFERENCES [App_DataDictionary].[ModelNameSpace] ([NameSpaceId]),
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd])
);

