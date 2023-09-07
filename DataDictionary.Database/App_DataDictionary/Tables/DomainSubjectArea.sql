/* -- Currently proof of concept.
   -- Envisioned as mechanism to organize Attributes and Entities into related units.
   -- Like how Database Catalog is the root of all Db stuff.
   -- Would replace the use of ModelAttribute and ModelEntity.
CREATE TABLE [App_DataDictionary].[DomainSubjectArea]
( 
	[SubjectAreaId]    UniqueIdentifier NOT Null,
	[SubjectAreaDescription] [App_DataDictionary].[typeDescription] Null,
	[SubjectAreaTitle] [App_DataDictionary].[typeTitle] Not Null,
	[ModelId]          UniqueIdentifier NOT NULL,
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy]     SysName Not Null CONSTRAINT [DF_DomainSubjectArea_ModfiedBy] DEFAULT (original_login()),
	[SysStart]      DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_DomainSubjectArea_SysStart] DEFAULT (sysdatetime()),
	[SysEnd]        DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_DomainSubjectArea_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_DomainSubjectArea] PRIMARY KEY CLUSTERED ([SubjectAreaId] ASC),
	CONSTRAINT [FK_DomainSubjectAreaModel] FOREIGN KEY ([ModelId]) REFERENCES [App_DataDictionary].[Model] ([ModelId]),
)
*/
