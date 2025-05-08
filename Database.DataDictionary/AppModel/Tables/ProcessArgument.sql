CREATE TABLE [AppModel].[ProcessArgument]
(
	[ProcessId]             UniqueIdentifier Not Null,
	[ArgumentId]			UniqueIdentifier Not Null CONSTRAINT [DF_ArgumentId] DEFAULT (newid()),
	[ArgumentTitle]			[App_DataDictionary].[typeTitle]       Not Null,
	[ArgumentDescription]	[App_DataDictionary].[typeDescription] Null,
	[ArgumentName]			[AppModel].[typeQualifiedName]         Null, -- This Arguments name
	[ArgumentType]			[AppModel].[typeQualifiedName]         Null, -- An Entity, Attribute, or system name
	[OrdinalPosition]       Int Not Null,
	[IsInput]               Bit Null, -- Input Can be bidirectional or not defined (contributes)
	[IsOutput]              Bit Null, -- Output Can be bidirectional or not defined (contributes)
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