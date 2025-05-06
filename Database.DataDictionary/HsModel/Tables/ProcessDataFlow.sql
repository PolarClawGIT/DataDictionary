CREATE TABLE [HsModel].[ProcessDataFlow]
(
	[ProcessId]         UniqueIdentifier Not Null,
	[DataFlowAliasId]   UniqueIdentifier Not Null,
	[DataFlowKnownAs]	[App_DataDictionary].[typeTitle] Not Null,
	[IsInFlow]          Bit Null,
	[IsOutFlow]         Bit Null,
	[SysStart]          DateTime2 (7) NOT NULL,
	[SysEnd]            DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_ProcessDataFlow]
    ON [HsModel].[ProcessDataFlow]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_ProcessDataFlow]
    ON [HsModel].[ProcessDataFlow]([ProcessId] ASC, [DataFlowAliasId] ASC)
GO