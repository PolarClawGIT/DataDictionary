CREATE TABLE [AppModel].[ModelAttribute] (
    [ModelId]       UniqueIdentifier                                   NOT NULL,
    [AttributeId]   UniqueIdentifier                                   NOT NULL,
    [SysStart]      DateTime2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ModelAttribute_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DateTime2 (7) GENERATED ALWAYS AS ROW END HIDDEN CONSTRAINT [DF_ModelAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    CONSTRAINT [PK_ModelAttribute] PRIMARY KEY CLUSTERED ([ModelId] ASC, [AttributeId] ASC),
    CONSTRAINT [FK_ModelAttribute_Attribute] FOREIGN KEY ([AttributeId]) REFERENCES [AppModel].[Attribute] ([AttributeId]),
    CONSTRAINT [FK_ModelAttribute_Model] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[ModelAttribute]))
GO
