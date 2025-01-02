CREATE TABLE [AppModel].[ModelProperty]
(
	[ModelId]       UniqueIdentifier NOT NULL,
	[PropertyId]    UniqueIdentifier NOT NULL,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelProperty_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelProperty_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelProperty] PRIMARY KEY ([ModelId] ASC, [PropertyId] ASC),
	CONSTRAINT [FK_ModelProperty_Model] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelProperty_Property] FOREIGN KEY ([PropertyId]) REFERENCES [AppModel].[PropertyEnumeration] ([PropertyId]),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[ModelProperty]))