CREATE TABLE [AppPOC].[TemplateObject]
(	-- List of objects in the Template.
	[ObjectId]			UniqueIdentifier Not Null CONSTRAINT [DF_TemplateObjectId] DEFAULT (newid()),
	[TemplateId]		UniqueIdentifier Not Null,
	--[ModelId]			UniqueIdentifier Not Null, -- Objects is Model specific Each Model can have muliple Data for the same template.
	-- TODO: How to turn this into a filter mechanism?
	[ObjectScope]		[AppGeneral].[uddtScopeName] Null, -- Application Scope to match to. Required on the Leaf Node only. Null = Use Child Nodes Scope.
	[ObjectMember]		[AppGeneral].[uddtMember] Not Null, -- Member Name of the alias. Combined to create a NameSpace.
	[ParentObjectId]	UniqueIdentifier NULL,
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_TemplateObject_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_TemplateObject_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_TemplateObject] PRIMARY KEY CLUSTERED ([ObjectId] ASC),
	CONSTRAINT [FK_TemplateObjectTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [AppPOC].[Template] ([TemplateId]),
	--CONSTRAINT [FK_TemplateObjectModel] FOREIGN KEY ([ModelId]) REFERENCES [AppModel].[Model] ([ModelId]),

	CONSTRAINT [FK_TemplateObjectParent] FOREIGN KEY ([ObjectId]) REFERENCES [AppPOC].[TemplateObject] ([ObjectId]),

)
