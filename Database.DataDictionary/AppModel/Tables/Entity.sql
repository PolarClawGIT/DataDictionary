CREATE TABLE [AppModel].[Entity]
(
	-- In ER Diagrams, an Entity is a Table, View, or other supported object type.
	-- In DFD Diagrams, an Entity is the Data Flow and Data Store. It may also be a Terminator.
	[EntityId]          UniqueIdentifier Not Null CONSTRAINT [DF_EntityId] DEFAULT (newid()),
	[EntityTitle]       [AppGeneral].[dtTitle] Not Null,
	[EntityDescription] [AppGeneral].[dtDescription] Null,
	[EntityName]        [AppModel].[typeQualifiedName]         Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Entity_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Entity_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Entity] PRIMARY KEY CLUSTERED ([EntityId] ASC),
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[Entity]))
GO

