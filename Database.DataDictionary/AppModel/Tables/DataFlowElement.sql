CREATE TABLE [AppModel].[DataFlowElement]
(	-- Used to define what Information (elements) are part of the DataFlow
	[DataFlowElementId]  UniqueIdentifier Not Null CONSTRAINT [DF_DataFlowElementId] DEFAULT (newid()),
	[DataFlowId]         UniqueIdentifier Not Null,
	[EntityName]         [AppModel].[typeQualifiedName] Null,
	[AttributeName]      [AppModel].[typeQualifiedName] Null,
	-- TODO: Add System Version later once the schema is locked down
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DataFlowElement_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DataFlowElement_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DataFlowElement] PRIMARY KEY CLUSTERED ([DataFlowElementId] ASC),	
	CONSTRAINT [FK_DataFlowElement_DataFlow] FOREIGN KEY ([DataFlowId]) REFERENCES [AppModel].[DataFlow] ([DataFlowId]),

)
