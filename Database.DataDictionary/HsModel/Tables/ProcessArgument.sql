CREATE TABLE [HsModel].[ProcessArgument]
(
	[ProcessId]             UniqueIdentifier Not Null,
	[ArgumentAliasId]		UniqueIdentifier Not Null,
	[ArgumentKnownAs]		[AppGeneral].[dtTitle] Not Null,
	[OrdinalPosition]       Int Not Null,
	[IsPassed]				Bit Not Null,
	[IsReturned]			Bit Not Null,
	[IsContributor]			Bit Not Null,
	[IsAltered]				Bit Not Null,
	[AsValue]				Bit Not Null,
	[AsReference]			Bit Not Null,
	[SysStart]              DateTime2 (7) NOT NULL,
	[SysEnd]                DateTime2 (7) NOT NULL,
)
GO
CREATE CLUSTERED INDEX [IX_ProcessArgument]
    ON [HsModel].[ProcessArgument]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_PProcessArgument]
    ON [HsModel].[ProcessArgument]([ProcessId] ASC, [ArgumentAliasId] ASC)
GO
