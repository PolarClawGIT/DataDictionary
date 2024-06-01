CREATE TABLE [App_DataDictionary].[DomainProperty]
(
	-- Works as a lookup to create/define an Extended Property.
	[PropertyId]             UniqueIdentifier NOT NULL CONSTRAINT [DF_DomainPropertyId] DEFAULT (newid()),
	[PropertyTitle]          [App_DataDictionary].[typeTitle] Not Null, -- Title of the Property as it appears in the application. This may contain the Property Name but must be unique for each type of Extended Property it applies to.
	[PropertyDescription]    [App_DataDictionary].[typeDescription] Null,
	[IsCommon]               Bit Not Null DEFAULT(0), -- Common Properties are shared by all Models.
	-- Note: IsCommon Properties cannot be deleted or updated using the stored procedures. They must be modified directly.
	--       IsCommon flag must also be set directly. This avoids the application accidentally changing these.
	[DataType]               NVarChar(20) Not Null, -- Sub-Type of the Property. Types are defined in Application.
	-- Known: String, Integer, List, XML, MS_Description
	[PropertyData]           NVarChar(2000) Null, -- Data based on Type. Example is Procedure Name or Choice List. Managed by Application.
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_DomainPropertyModfiedBy] DEFAULT (ORIGINAL_LOGIN()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_DomainProperty_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_DomainProperty_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainProperty] PRIMARY KEY CLUSTERED ([PropertyId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_DomainProperty]
    ON [App_DataDictionary].[DomainProperty]([PropertyTitle] ASC);
GO