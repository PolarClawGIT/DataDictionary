CREATE TABLE [App_DataDictionary].[DomainSource]
(
	[SourceId]                  UniqueIdentifier NOT NULL CONSTRAINT [DF_DomainSourceId] DEFAULT (newid()),
	[SourceTitle]               [App_DataDictionary].[typeTitle] Not Null,
	[SourceDescription]         [App_DataDictionary].[typeDescription] Null,
	[SourceName]                As (Coalesce([DatabaseName], [AssemblyName])) PERSISTED,
	 -- Sub-Type reference to DatabaseName or Assembly Name. This is a Zero or One to Zero or One relationship.
	[DatabaseName]              SysName Null,
	[AssemblyName]              NVarChar(128) Null,
	[IsCaseSensitive]           Bit Not Null, -- Use a Case Sensitive compare instead 
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainSource_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainSource_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainSource_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_DomainSource] PRIMARY KEY CLUSTERED ([SourceId] ASC),
	CONSTRAINT [CK_DomainSource] CHECK (
		([DatabaseName] is Not Null And [AssemblyName] is Null) Or
		([DatabaseName] is Null And [AssemblyName] is Not Null))
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainSourceTitle]
    ON [App_DataDictionary].[DomainSource]([SourceTitle]);
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainSourceName]
    ON [App_DataDictionary].[DomainSource]([SourceName]);
GO