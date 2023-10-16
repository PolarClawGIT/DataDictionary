CREATE TABLE [App_DataDictionary].[ModelAttribute]
(
	[ModelId] UniqueIdentifier NOT NULL,
	[AttributeId] UniqueIdentifier NOT NULL,
	[SubjectAreaId] UniqueIdentifier Null,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ModelAttribute_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ModelAttribute_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ModelAttribute_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_ModelAttribute] PRIMARY KEY CLUSTERED ([ModelId] ASC, [AttributeId] ASC),
	CONSTRAINT [FK_ModelAttributeModel] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
	CONSTRAINT [FK_ModelAttributeDomainAttribute] FOREIGN KEY ([AttributeId]) REFERENCES [App_DataDictionary].[DomainAttribute] ([AttributeId]),
	CONSTRAINT [FK_ModelSubjectAreaAttribute] FOREIGN KEY ([ModelId], [SubjectAreaId]) REFERENCES [App_DataDictionary].[ModelSubjectArea] ([ModelId], [SubjectAreaId]),

)
