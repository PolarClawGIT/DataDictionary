CREATE TABLE [AppModel].[AttributeProperty]
(
	[AttributeId]		UniqueIdentifier Not Null,
	[PropertyId]		UniqueIdentifier NOT Null,
	[PropertyValue]		NVarChar(4000) Null, -- The Value for the Property. (Summary Text, Extended Property, Choice)
	-- TODO: Add System Version later once the schema is locked down
	[SysStart]			DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_AttributeProperty_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]			DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_AttributeProperty_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_AttributeProperty] PRIMARY KEY CLUSTERED ([AttributeId] ASC, [PropertyId] ASC),
	CONSTRAINT [FK_AttributePropertyDomainAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [AppModel].[Attribute] ([AttributeId]),
	CONSTRAINT [FK_AttributePropertyApplicationProperty] FOREIGN KEY ([PropertyId]) REFERENCES [AppModel].[PropertyEnumeration] ([PropertyId]),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[AttributeProperty]))
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_AttributeProperty]
    ON [AppModel].[AttributeProperty]([AttributeId] ASC, [PropertyId] ASC);
GO
