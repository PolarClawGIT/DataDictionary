CREATE TABLE [App_DataDictionary].[DomainProcessFlow]
(
	[ProcessFlowId]           UniqueIdentifier Not Null CONSTRAINT [DF_DomainProcessFlowId] DEFAULT (newid()),
	[ProcessFlowTitle]       [App_DataDictionary].[typeTitle] Not Null,
	[ProcessFlowDescription] [App_DataDictionary].[typeDescription] Null,
	[ProcessId]              UniqueIdentifier Not Null,
	[EntityId]               UniqueIdentifier Null,
	[AttributeId]            UniqueIdentifier Null,
	[IsInflow]               Bit Not Null, -- Inflow/Input Can be bidirectional or not defined (contributes)
	[IsOutflow]              Bit Not Null, -- Outflow/Output Can be bidirectional or not defined (contributes)
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainProcessFlow_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainProcessFlow_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainProcessFlow_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainProcessFlow] PRIMARY KEY CLUSTERED ([ProcessFlowId] ASC),	
	CONSTRAINT [FK_DomainProcessFlow_Process] FOREIGN KEY ([ProcessId]) REFERENCES [App_DataDictionary].[DomainProcess] ([ProcessId]),
	CONSTRAINT [FK_DomainProcessFlow_Attribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
	CONSTRAINT [FK_DomainProcessFlow_Entity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
)
