CREATE TABLE [AppPOC].[Template]
(	-- Describes the Template
	[TemplateId]            UniqueIdentifier Not Null CONSTRAINT [DF_TemplateId] DEFAULT (newid()),
	[TemplateTitle]			[AppGeneral].[uddtTitle] Not Null,
	[TemplateDescription]	[AppGeneral].[uddtDescription] Null,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Template_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Template_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Template] PRIMARY KEY CLUSTERED ([TemplateId] ASC),
	CONSTRAINT [AK_TemplateTitle] UNIQUE ([TemplateTitle] ASC),
)
