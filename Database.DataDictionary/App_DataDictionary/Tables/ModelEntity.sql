CREATE TABLE [App_DataDictionary].[ModelEntity]
(
	[ModelId] UniqueIdentifier NOT NULL,
	[EntityId] UniqueIdentifier NOT NULL,
	[SubjectAreaId] UniqueIdentifier Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ModelEntity_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelEntity_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelEntity_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelEntity] PRIMARY KEY CLUSTERED ([ModelId] ASC, [EntityId] ASC),
	CONSTRAINT [FK_ModelEntity_Model] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelDomainEntity] FOREIGN KEY ([EntityId]) REFERENCES [App_DataDictionary].[DomainEntity] ([EntityId]),
	CONSTRAINT [FK_ModelSubjectAreaEntity] FOREIGN KEY ([ModelId], [SubjectAreaId]) REFERENCES [App_DataDictionary].[ModelSubjectArea] ([ModelId], [SubjectAreaId]),
)
