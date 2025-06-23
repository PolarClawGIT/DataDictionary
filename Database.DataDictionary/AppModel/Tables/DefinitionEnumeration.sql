CREATE TABLE [AppModel].[DefinitionEnumeration]
(
	[DefinitionId]             UniqueIdentifier NOT NULL CONSTRAINT [DF_DefinitionId] DEFAULT (newid()),
	[DefinitionTitle]          [AppGeneral].[uddtTitle] Not Null, -- Title of the Definition as it appears in the application. This may contain the Property Name but must be unique for each type of Extended Property it applies to.
	[DefinitionDescription]    [AppGeneral].[uddtDescription] Null,
	[IsCommon]                 Bit Not Null CONSTRAINT [DF_DefinitionIsCommon] DEFAULT(0), -- Common Definitions are shared by all Models.
	-- Note: IsCommon Definitions cannot be deleted or updated using the stored procedures. They must be modified directly.
	--       IsCommon flag must also be set directly. This avoids the application accidentally changing these.
	-- Temporal History Support
	[SysStart]                 DateTime2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_Definition_SysStart] Default (sysdatetime()),
	[SysEnd]                   DateTime2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_Definition_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Definition] PRIMARY KEY CLUSTERED ([DefinitionId] ASC),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[DefinitionEnumeration]))
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Definition]
    ON [AppModel].[DefinitionEnumeration]([DefinitionTitle] ASC);
GO