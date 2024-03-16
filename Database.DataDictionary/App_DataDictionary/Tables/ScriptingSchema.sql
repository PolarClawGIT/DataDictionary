CREATE TABLE [App_DataDictionary].[ScriptingSchema]
(	-- This serves as the XSD or XML Schema Definition
	[SchemaId]             UniqueIdentifier NOT NULL CONSTRAINT [DF_ScriptingSchemaId] DEFAULT (newid()),
	[SchemaTitle]          [App_DataDictionary].[typeTitle] Not Null,
	[SchemaDescription]    [App_DataDictionary].[typeDescription] Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ScriptingSchema_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ScriptingSchema_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ScriptingSchema_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ScriptingSchema] PRIMARY KEY CLUSTERED ([SchemaId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ScriptingSchema]
    ON [App_DataDictionary].[ScriptingSchema]([SchemaTitle]);
GO
