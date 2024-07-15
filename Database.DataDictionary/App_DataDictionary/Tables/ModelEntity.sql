CREATE TABLE [App_DataDictionary].[ModelEntity] (
    [ModelId]       UNIQUEIDENTIFIER                                   NOT NULL,
    [EntityId]      UNIQUEIDENTIFIER                                   NOT NULL,
    [SubjectAreaId] UNIQUEIDENTIFIER                                   NULL,
    [MemberName]    [App_DataDictionary].[typeNameSpaceMember]         NOT NULL,
    [ModfiedBy]     [sysname]                                          CONSTRAINT [DF_ModelEntity_ModfiedBy] DEFAULT (original_login()) NOT NULL,
    [SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ModelEntity_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_ModelEntity_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    CONSTRAINT [PK_ModelEntity] PRIMARY KEY CLUSTERED ([ModelId] ASC, [EntityId] ASC),
    CONSTRAINT [FK_ModelEntity_Entity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
    CONSTRAINT [FK_ModelEntity_Model] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
    CONSTRAINT [AK_ModelEntity] UNIQUE NONCLUSTERED ([ModelId] ASC, [MemberName] ASC),
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd])
);


GO
