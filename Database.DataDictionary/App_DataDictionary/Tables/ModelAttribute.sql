CREATE TABLE [App_DataDictionary].[ModelAttribute] (
    [ModelId]       UNIQUEIDENTIFIER                                   NOT NULL,
    [AttributeId]   UNIQUEIDENTIFIER                                   NOT NULL,
    [MemberName]    [App_DataDictionary].[typeNameSpaceMember]         NOT NULL, -- Combined with SubjectArea NameSpace to create a full path
    [ModfiedBy]     [sysname]                                          CONSTRAINT [DF_ModelAttribute_ModfiedBy] DEFAULT (original_login()) NOT NULL,
    [SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ModelAttribute_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_ModelAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    CONSTRAINT [PK_ModelAttribute] PRIMARY KEY CLUSTERED ([ModelId] ASC, [AttributeId] ASC),
    CONSTRAINT [FK_ModelAttribute_Attribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
    CONSTRAINT [FK_ModelAttribute_Model] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
    CONSTRAINT [AK_ModelAttribute] UNIQUE NONCLUSTERED ([ModelId] ASC, [MemberName] ASC),
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd])
);


GO
