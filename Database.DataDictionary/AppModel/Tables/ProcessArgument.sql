CREATE TABLE [AppModel].[ProcessArgument]
(
	[ProcessId]             UniqueIdentifier Not Null,
	[ArgumentId]			UniqueIdentifier Not Null CONSTRAINT [DF_ArgumentId] DEFAULT (newid()),
	[ArgumentTitle]			[App_DataDictionary].[typeTitle]       Not Null,
	[ArgumentDescription]	[App_DataDictionary].[typeDescription] Null,
	[ArgumentName]			[AppModel].[typeQualifiedName]         Null, -- This is the Entity or Attribute Name
	[OrdinalPosition]       Int Not Null,
	[IsPassed]				Bit Not Null CONSTRAINT [DF_ProcessArgumentPassed] DEFAULT (0), -- The Argument is passed to the process (Input)
	[IsReturned]			Bit Not Null CONSTRAINT [DF_ProcessArgumentReturned] DEFAULT (0), -- The Argument is returned by the process (Output)
	[IsContributor]			Bit Not Null CONSTRAINT [DF_ProcessArgumentContributor] DEFAULT (0), -- The Argument Contributes by the process (Input)
	[IsAltered]				Bit Not Null CONSTRAINT [DF_ProcessArgumentAltered] DEFAULT (0), -- The Argument is altered by the process (Input/Output)
	[AsValue]				Bit Not Null CONSTRAINT [DF_ProcessArgumentValue] DEFAULT (0), -- Is the Argument a Value (SQL Type or .net base type)
	[AsReference]			Bit Not Null CONSTRAINT [DF_ProcessArgumentReference] DEFAULT (0), -- Is the Argument a Reference (.Net class or other reference)
	--[IsInput] As Convert(Bit, Case When [IsPassed] = 1 or [IsContributor] = 1 Then 1 Else 0 End),
	--[IsOutput] As Convert(Bit, Case When [IsReturned] = 1 or [IsAltered] = 1 Then 1 Else 0 End),
    -- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ProcessArgument_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ProcessArgument_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ProcessArgument] PRIMARY KEY CLUSTERED ([ProcessId] ASC, [ArgumentId] ASC ),	
	CONSTRAINT [FK_ProcessArgument_Process] FOREIGN KEY ([ProcessId]) REFERENCES [AppModel].[Process] ([ProcessId]),
	CONSTRAINT [AK_ProcessArgumentTitle] UNIQUE ([ProcessId] ASC, [ArgumentTitle] ASC),
	CONSTRAINT [AK_ProcessArgumentPosition] UNIQUE ([ProcessId] ASC, [OrdinalPosition] ASC),
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[ProcessArgument]))
GO