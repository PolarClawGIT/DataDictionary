CREATE TABLE [AppModel].[PropertyEnumeration]
(
	-- Works as a lookup to create/define an Extended Property.
	[PropertyId]             UniqueIdentifier NOT NULL CONSTRAINT [DF_PropertyTypeId] DEFAULT (newid()),
	[PropertyTitle]          [App_DataDictionary].[typeTitle] Not Null, -- Title of the Property as it appears in the application. This may contain the Property Name but must be unique for each type of Extended Property it applies to.
	[PropertyDescription]    [App_DataDictionary].[typeDescription] Null,
	[IsCommon]               Bit Not Null DEFAULT(0), -- Common Properties are shared by all Models.
	-- Note: IsCommon Properties cannot be deleted or updated using the stored procedures. They must be modified directly.
	--       IsCommon flag must also be set directly. This avoids the application accidentally changing these.
	[DataType]               NVarChar(20) Not Null, -- Sub-Type of the Property. Types are defined in Application.
	-- Known: String, Integer, List, XML, MS_Description
	[PropertyData]           NVarChar(2000) Null, -- Data based on Type. Example is Procedure Name or Choice List. Managed by Application.
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_PropertyType_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_PropertyType_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_PropertyType] PRIMARY KEY CLUSTERED ([PropertyId] ASC),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsModel].[PropertyEnumeration]))
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_PropertyType]
    ON [AppModel].[PropertyEnumeration]([PropertyTitle] ASC);
GO