CREATE TABLE [AppModel].[Model]
(
	-- The Model represent the Entity Relationship (ERD) and Data Flow (DFD) side of the application.
	-- The components of the Model are associated with zero, one, or more models.
	-- The Model contains the majority of the user-editable data.
	-- The application works with a single model at a time.
	[ModelId]          UniqueIdentifier NOT NULL CONSTRAINT [DF_ModelId] DEFAULT (newid()),
	[ModelTitle]       [App_DataDictionary].[typeTitle] Not Null,
	[ModelDescription] [App_DataDictionary].[typeDescription] Null,
	-- Temporal History Support
	[SysStart]         DateTime2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Model_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]           DateTime2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Model_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Model] PRIMARY KEY CLUSTERED ([ModelId] ASC),
) 
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[Model]))
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Model]
    ON [AppModel].[Model]([ModelTitle] ASC);
GO