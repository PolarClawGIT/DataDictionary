CREATE TABLE [App_DataDictionary].[Model]
(
	-- This is equivalent to a file within the file systems. Instead of saving to the file system, the data is saved to the database.
	[ModelId] UniqueIdentifier NOT NULL CONSTRAINT [DF_ModelId] DEFAULT (newid()),
	[ModelTitle] [App_DataDictionary].[typeTitle] Not Null,
	[ModelDescription] [App_DataDictionary].[typeDescription] Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName Not Null CONSTRAINT [DF_Model_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Model_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Model_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Model] PRIMARY KEY CLUSTERED ([ModelId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Model]
    ON [App_DataDictionary].[Model]([ModelTitle] ASC);
GO