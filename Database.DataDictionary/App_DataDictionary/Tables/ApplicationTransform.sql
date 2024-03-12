CREATE TABLE [App_DataDictionary].[ApplicationTransform]
(
    -- This represents the data for the Scripting Engine. 
	-- The Engine uses XML Transform (XSLT) to do the work.
	-- The results is Text or XML.
	[TransformId]             UniqueIdentifier NOT NULL CONSTRAINT [DF_ApplicationTransformId] DEFAULT (newid()),
	[TransformTitle]          [App_DataDictionary].[typeTitle] Not Null, -- Title of the Property as it appears in the application. This may contain the Property Name but must be unique for each type of Extended Property it applies to.
	[TransformDescription]    [App_DataDictionary].[typeDescription] Null,
	[ScopeId]                 Int Not Null,
	[AsText]                  Bit Null, -- The result is expected to be plain text. 0 = XML, 1 = Text
	--	[AsXml]         As (convert(bit, case when [AsText]=(0) then (1) when [AsText]=(1) then (0) end)),
	[TransformScript]         XML Null, -- XSLT Transform Script
	-- TODO: Add System Version later once the schema is locked down
	[ModfiedBy] SysName Not Null CONSTRAINT [DF_ApplicationTransform_ModfiedBy] DEFAULT (original_login()),
	[SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL CONSTRAINT [DF_ApplicationTransform_SysStart] DEFAULT (sysdatetime()),
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL CONSTRAINT [DF_ApplicationTransform_SysEnd] DEFAULT ('9999-12-31 23:59:59.9999999'),
   	PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
	CONSTRAINT [PK_ApplicationTransform] PRIMARY KEY CLUSTERED ([TransformId] ASC),
	CONSTRAINT [FK_ApplicationTransformScope] FOREIGN KEY ([ScopeId]) REFERENCES [App_DataDictionary].[ApplicationScope] ([ScopeId]),

)

