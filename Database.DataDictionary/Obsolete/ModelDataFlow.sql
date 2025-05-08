CREATE TABLE [AppModel].[ModelDataFlow]
(
    [ModelId]       UNIQUEIDENTIFIER NOT NULL,
    [DataFlowId]    UNIQUEIDENTIFIER NOT NULL,
    [SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT [DF_ModelDataFlow_SysStart] DEFAULT (sysdatetime()) NOT NULL,
    [SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN   CONSTRAINT [DF_ModelDataFlow_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999') NOT NULL,
    CONSTRAINT [PK_ModelDataFlow] PRIMARY KEY CLUSTERED ([ModelId] ASC, [DataFlowId] ASC),
    CONSTRAINT [FK_ModelDataFlow_DataFlow] FOREIGN KEY ([DataFlowId]) REFERENCES [AppModel].[DataFlow] ([DataFlowId]),
    CONSTRAINT [FK_ModelDataFlow_Model] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd])
)
