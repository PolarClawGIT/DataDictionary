CREATE TABLE [AppModel].[DataFlow]
(	-- Represents a flow of information between Processes, Terminators or Warehouses (Entities/Attributes)
	[DataFlowId]             UniqueIdentifier Not Null CONSTRAINT [DF_DataFlowId] DEFAULT (newid()),
	[DataFlowTitle]          [App_DataDictionary].[typeTitle] Not Null,
	[DataFlowDescription]    [App_DataDictionary].[typeDescription] Null,
	[DataFlowName]           [AppModel].[typeQualifiedName]         Null,
	[IsExternal]             Bit Null, -- Is the Data coming/going to a External element (Sink/Source or other Terminator).
	[IsControlFlow]          Bit Null, -- Causes control to flow to the next process. Generally no data is involved.
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DataFlow_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DataFlow_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DataFlow] PRIMARY KEY CLUSTERED ([DataFlowId] ASC),
)
