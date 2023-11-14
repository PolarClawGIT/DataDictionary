CREATE TABLE [App_DataDictionary].[AliasSource]
(
	[AliasSourceId]             UniqueIdentifier NOT NULL CONSTRAINT [DF_AliasSourceId] DEFAULT (newid()),
	[AliasSourceTitle]          [App_DataDictionary].[typeTitle] Not Null,
	[AliasSourceDescription]    [App_DataDictionary].[typeDescription] Null,
	[SourceName]                 As (Coalesce([DatabaseName], [AssemblyName])) PERSISTED,
	 -- Sub-Type reference to DatabaseName or Assembly Name. This is a Zero or One to Zero or One relationship.
	[DatabaseName]              SysName Null,
	[AssemblyName]              NVarChar(128) Null,
	[IsCaseSensitive]           Bit Not Null, -- Use a Case Sensitive compare instead 
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_AliasSource_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_AliasSource_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_AliasSource_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_AliasSource] PRIMARY KEY CLUSTERED ([AliasSourceId] ASC),
	CONSTRAINT [CK_AliasSource] CHECK (
		([DatabaseName] is Not Null And [AssemblyName] is Null) Or
		([DatabaseName] is Null And [AssemblyName] is Not Null))
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_AliasSourceTitle]
    ON [App_DataDictionary].[AliasSource]([AliasSourceTitle]);
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_AliasSourceName]
    ON [App_DataDictionary].[AliasSource]([SourceName]);
GO