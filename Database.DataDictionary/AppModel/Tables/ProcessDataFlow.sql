CREATE TABLE [AppModel].[ProcessDataFlow]
(	-- For a given process, what are the Input/Output values (aka DataFlows).
	[ProcessId]              UniqueIdentifier Not Null,
	[DataFlowAliasId]        UniqueIdentifier Not Null, -- Entity, Attribute or Relationship of the flow
	[DataFlowKnownAs]	     [App_DataDictionary].[typeTitle] Not Null, -- What to call the object within this DataFlows (default is the Attribute/Entity Name)
	[IsInFlow]               Bit Null, -- Inflow/Input Can be bidirectional or not defined (contributes)
	[IsOutFlow]              Bit Null, -- Outflow/Output Can be bidirectional or not defined (contributes)
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ProcessDataFlow_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ProcessDataFlow_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ProcessDataFlow] PRIMARY KEY CLUSTERED ([ProcessId] ASC, [DataFlowAliasId] ASC ),	
	CONSTRAINT [FK_ProcessDataFlow_Process] FOREIGN KEY ([ProcessId]) REFERENCES [AppModel].[Process] ([ProcessId]),
	CONSTRAINT [FK_ProcessDataFlow_Alias] FOREIGN KEY ([DataFlowAliasId]) REFERENCES [AppModel].[AliasHierarchy] ([AliasId]),
	CONSTRAINT [AK_ProcessDataFlowTitle] UNIQUE ([ProcessId] ASC, [DataFlowKnownAs] ASC),
)
