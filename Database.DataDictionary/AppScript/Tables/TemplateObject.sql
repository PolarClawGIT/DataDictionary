CREATE TABLE [AppScript].[TemplateObject]
(	-- Object of the Template. Muliple Documents may refrence the same Object.
	-- The Leaf level Node is expected to have all attributes but non-child nodes may not have all the details.
	[ObjectId]			UniqueIdentifier Not Null CONSTRAINT [DF_ObjectId] DEFAULT (newid()),
	[TemplateId]		UniqueIdentifier Not Null,
	-- Object Filter/Matching
	[ParentObjectId]	UniqueIdentifier NULL,
	[ObjectScope]		[AppGeneral].[uddtScopeName] Null, -- ScopeType for the Object
	--[ObjectPath]		[AppGeneral].[uddtPath] Null, -- Varies by Model, Null = Model Root
	[ObjectMember]		[AppGeneral].[uddtMember] Not Null, -- Member Name of the Object
	-- Behavior 		TODO: This might be solved diffrently by assocating an Object to a Model. Right now, this is just for application processing but even that may not be needed.
	[IsExcluded]		Bit Not Null CONSTRAINT [DF_Object_IsExcluded] DEFAULT (0),  -- Do not generate a File for this object.
	[KeepOrphaned]		Bit Not NULL CONSTRAINT [DF_Object_KeepOrphaned] DEFAULT (0), -- Keep this object even if oprhaned, otherwise delete if oprhaned.
	-- Temporal History Support
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_Object_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_Object_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	-- Keys
	CONSTRAINT [PK_Object] PRIMARY KEY CLUSTERED ([ObjectId] ASC),
	CONSTRAINT [AK_Object] UNIQUE ([TemplateId] ASC, [ObjectId] ASC), -- Used by FK's
	CONSTRAINT [FK_ObjectTemplate] FOREIGN KEY ([TemplateId]) REFERENCES [AppScript].[Template] ([TemplateId]),
	CONSTRAINT [FK_ObjectParent] FOREIGN KEY ([ParentObjectId]) REFERENCES [AppScript].[TemplateObject] ([ObjectId]),
	CONSTRAINT [AK_DocumentObjectName] UNIQUE ([TemplateId] ASC, [ParentObjectId] ASC, [ObjectMember] ASC),
)	WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [HsScript].[TemplateObject]))
