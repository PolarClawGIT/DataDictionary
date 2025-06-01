CREATE TABLE [AppModel].[ModelProcess] (
    [ModelId]       UNIQUEIDENTIFIER                                   NOT NULL,
    [ProcessId]     UNIQUEIDENTIFIER                                   NOT NULL,
    -- TODO: Add System Version later once the schema is locked down
    [SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ModelProcess_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_ModelProcess_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    CONSTRAINT [PK_ModelProcess] PRIMARY KEY CLUSTERED ([ModelId] ASC, [ProcessId] ASC),
    CONSTRAINT [FK_ModelProcess_Model] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
    CONSTRAINT [FK_ModelProcess_Process] FOREIGN KEY ([ProcessId]) REFERENCES [AppModel].[Process] ([ProcessId]),
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd])
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[ModelProcess]))
GO