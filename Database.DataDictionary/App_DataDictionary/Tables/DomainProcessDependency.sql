CREATE TABLE [App_DataDictionary].[DomainProcessDependency]
(
	[ProcessId] UniqueIdentifier Not Null,
	[ProcessDependencyId] Int Not Null, -- Surrogate Key element, maintained by Database.
	[ProcessDependencyTitle] [App_DataDictionary].[typeTitle] Not Null,
	-- Not-normalized to better translate to SQL call.
	[DatabaseName] SysName Not Null, -- Database/Catalog Name
	[SchemaName] SysName Not Null, -- Name of Level0. Examples is Schema, Synonym, and User Name. For attributes, this is always a SchemaName.
	[ObjectName] SysName Not Null, -- Name of Level1. Examples are Table, View, Procedure and Function Name
	[ElementName] SysName Null, -- Name of Level2. Examples are Column and Parameter Name
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainProcessDependency_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainProcessDependency_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainProcessDependency_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainProcessDependency] PRIMARY KEY CLUSTERED ([ProcessId] ASC),	
)
