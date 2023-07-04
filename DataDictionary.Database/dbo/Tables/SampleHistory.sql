CREATE TABLE [dbo].[SampleTemporal]
(
    [SampleTemporalId]    INT            NOT NULL,
    [SampleTemporalTitle] NVARCHAR (100) NOT NULL,
    [SampleTemporalData]  NVARCHAR (MAX) NULL,
    -- History
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainAttribute_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_SampleTemporal] PRIMARY KEY CLUSTERED ([SampleTemporalId] ASC),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[SampleHistory]));
