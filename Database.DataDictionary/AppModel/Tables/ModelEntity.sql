CREATE TABLE [AppModel].[ModelEntity] (
    [ModelId]       UNIQUEIDENTIFIER                                   NOT NULL,
    [EntityId]      UNIQUEIDENTIFIER                                   NOT NULL,
    [SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ModelEntity_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_ModelEntity_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    CONSTRAINT [PK_ModelEntity] PRIMARY KEY CLUSTERED ([ModelId] ASC, [EntityId] ASC),
    CONSTRAINT [FK_ModelEntity_Entity] FOREIGN KEY ([EntityId]) REFERENCES [AppModel].[Entity] ([EntityId]),
    CONSTRAINT [FK_ModelEntity_Model] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[ModelEntity]))
GO
