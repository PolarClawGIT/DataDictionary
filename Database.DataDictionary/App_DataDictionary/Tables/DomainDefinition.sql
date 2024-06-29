CREATE TABLE [App_DataDictionary].[DomainDefinition]
(
	[DefinitionId]             UniqueIdentifier NOT NULL CONSTRAINT [DF_DomainDefinitionId] DEFAULT (newid()),
	[DefinitionTitle]          [App_DataDictionary].[typeTitle] Not Null, -- Title of the Definition as it appears in the application. This may contain the Property Name but must be unique for each type of Extended Property it applies to.
	[DefinitionDescription]    [App_DataDictionary].[typeDescription] Null,
	[IsCommon]                 Bit Not Null DEFAULT(0), -- Common Definitions are shared by all Models.
	-- Note: IsCommon Definitions cannot be deleted or updated using the stored procedures. They must be modified directly.
	--       IsCommon flag must also be set directly. This avoids the application accidentally changing these.
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainDefinitionModfiedBy] DEFAULT (ORIGINAL_LOGIN()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_DomainDefinition_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_DomainDefinition_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainDefinition] PRIMARY KEY CLUSTERED ([DefinitionId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainDefinition]
    ON [App_DataDictionary].[DomainDefinition]([DefinitionTitle] ASC);
GO