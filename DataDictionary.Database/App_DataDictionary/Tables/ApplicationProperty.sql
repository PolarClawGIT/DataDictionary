﻿CREATE TABLE [App_DataDictionary].[ApplicationProperty]
(
	-- Works as a lookup to create/define an Extended Property.
	[PropertyId]             UniqueIdentifier NOT NULL CONSTRAINT [DF_ApplicationPropertyId] DEFAULT (newid()),
	[PropertyTitle]          [App_DataDictionary].[typeTitle] Not Null, -- Title of the Property as it appears in the application. This may contain the Property Name but must be unique for each type of Extended Property it applies to.
	[PropertyDescription]    [App_DataDictionary].[typeDescription] Null,
	-- Properties are sub-typed. This defines the options for the sub-types.
	-- This is a de-normalization choice to better accommodate a WinForm application
		[IsExtendedProperty] As (Convert([Bit], case when [ExtendedProperty] is Null then (0) else (1) end)), -- Helper Flag for code
		[IsDefinition]       Bit Null, -- Null or 0 = no Rich Text Definition allowed
		[IsChoice]           AS (Convert([Bit], Case When [ChoiceList] is Null then (0) else (1) End)), -- Helper Flag for code
		[ExtendedProperty]   SysName Null, -- Null do not populate an MS Extended Property. Name for the Extended property. Most interested in: MS_Description
		-- Choice Sub-Type
		[ChoiceList]         NVarChar(2000) Null, -- Null = no choices. Comma Separated List of Choices allowed (cannot not be verified)
	-- End of Sub-Types
	[Obsolete] As (CONVERT([bit],case when [ObsoleteDate] IS NULL then (0) else (1) end)),
	[ObsoleteDate] DATETIME2 Null, -- Used to flag an item as a candidate for being deleted. Null = active, anything else is Obsolete.
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ApplicationPropertyModfiedBy] DEFAULT (ORIGINAL_LOGIN()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL Constraint [DF_ApplicationProperty_SysStart] Default (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL Constraint [DF_ApplicationProperty_SysEnd] Default ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ApplicationProperty] PRIMARY KEY CLUSTERED ([PropertyId] ASC),
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ApplicationProperty]
    ON [App_DataDictionary].[ApplicationProperty]([PropertyTitle] ASC);
GO