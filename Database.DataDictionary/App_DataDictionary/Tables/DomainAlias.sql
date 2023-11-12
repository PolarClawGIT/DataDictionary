CREATE TABLE [App_DataDictionary].[DomainAlias]
(   -- This structurer is used to store the Alias data for Attributes and Entities.
	-- The Alias Name can be a Database Object (Database, Schema, Object, Element) or
	-- Library NameSpace (NameSpace, Class, Method, Property).
	-- There is one Alias list maintained per Model.
	-- This structure is a hierarchy.
	[AliasId] UniqueIdentifier Not Null CONSTRAINT [DF_DomainAliasId] DEFAULT (newsequentialid()),
	[ModelId] UniqueIdentifier NOT NULL,
	[AliasTitle] [App_DataDictionary].[typeTitle] Not Null,
	[AliasDescription] [App_DataDictionary].[typeDescription] Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainAlias_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainAlias_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainAlias_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainAlias] PRIMARY KEY CLUSTERED ([AliasId] ASC),
	CONSTRAINT [FK_DomainAlias_Model] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
)
GO