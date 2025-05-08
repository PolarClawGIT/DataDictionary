CREATE TABLE [HsModel].[ProcessArgument]
(
	[ProcessId]             UniqueIdentifier Not Null,
	[ArgumentId]			UniqueIdentifier Not Null,
	[ArgumentTitle]			[App_DataDictionary].[typeTitle]       Not Null,
	[ArgumentDescription]	[App_DataDictionary].[typeDescription] Null,
	[ArgumentName]			[AppModel].[typeQualifiedName]         Null,
	[ArgumentType]			[AppModel].[typeQualifiedName]         Null,
	[OrdinalPosition]       Int Not Null,
	[IsInput]               Bit Null,
	[IsOutput]              Bit Null,
	[SysStart]              DateTime2 (7) NOT NULL,
	[SysEnd]                DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_ProcessArgument]
    ON [HsModel].[ProcessArgument]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_PProcessArgument]
    ON [HsModel].[ProcessArgument]([ProcessId] ASC, [ArgumentId] ASC)
GO
