CREATE TABLE [App_DataDictionary].[ModelProperty]
(
	[ModelId]       UniqueIdentifier NOT NULL,
	[PropertyId]    UniqueIdentifier NOT NULL,
	-- TODO: Add System Version later once the schema is locked down
	[ModifiedBy] SysName NOT NULL CONSTRAINT [DF_ModelProperty_ModifiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelProperty_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelProperty_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelProperty] PRIMARY KEY ([ModelId] ASC, [PropertyId] ASC),
	CONSTRAINT [FK_ModelProperty_Model] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelProperty_Property] FOREIGN KEY ([PropertyId]) REFERENCES [App_DataDictionary].[DomainProperty] ([PropertyId]),
)
GO