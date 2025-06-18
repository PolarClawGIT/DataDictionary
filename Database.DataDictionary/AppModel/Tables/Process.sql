CREATE TABLE [AppModel].[Process]
(
	[ProcessId]          UniqueIdentifier Not Null CONSTRAINT [DF_ProcessId] DEFAULT (newid()),
	[ProcessTitle]       [AppGeneral].[typeTitle] Not Null,
	[ProcessDescription] [AppGeneral].[typeDescription] Null,
	[ProcessName]        [AppModel].[typeQualifiedName]         Null,
    -- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Process_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Process_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Process] PRIMARY KEY CLUSTERED ([ProcessId] ASC),
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[Process]))
GO
