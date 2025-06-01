CREATE TABLE [AppModel].[DataFlowProperty]
(
	[DataFlowId]		UniqueIdentifier Not Null,
	[PropertyId]		UniqueIdentifier NOT Null,
	[PropertyValue]		[AppModel].[typePropertyValue] Null, -- The Value for the Property. (Summary Text, Extended Property, Choice)
	-- Temporal History Support
	[SysStart]			DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DataFlowProperty_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]			DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DataFlowProperty_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DataFlowProperty] PRIMARY KEY CLUSTERED ([DataFlowId] ASC, [PropertyId] ASC),
	CONSTRAINT [FK_DataFlowPropertyDomainDataFlow] FOREIGN KEY ([DataFlowId]) REFERENCES [AppModel].[DataFlow] ([DataFlowId]),
	CONSTRAINT [FK_DataFlowPropertyApplicationProperty] FOREIGN KEY ([PropertyId]) REFERENCES [AppModel].[PropertyEnumeration] ([PropertyId]),
)
GO
