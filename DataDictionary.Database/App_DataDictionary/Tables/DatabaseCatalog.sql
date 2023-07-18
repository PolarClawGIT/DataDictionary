CREATE TABLE [App_DataDictionary].[DatabaseCatalog]
(
	[CatalogId] UniqueIdentifier Not Null CONSTRAINT [DF_DatabaseCatalog_CatalogId] DEFAULT (newid()),
	[CatalogName] SysName Not Null,
	[SourceServerName] SysName Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DatabaseCatalog_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DatabaseCatalog_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ADatabaseCatalog_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DatabaseCatalog] PRIMARY KEY CLUSTERED ([CatalogId] ASC),
)
/*
Select	Convert(UniqueIdentifier,Null) As [CatalogId],
		Db_Name() As [CatalogName],
		@Server As [SourceServerName]
*/