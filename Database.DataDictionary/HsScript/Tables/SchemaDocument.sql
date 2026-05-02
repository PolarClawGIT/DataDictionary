CREATE TABLE [HsScript].[SchemaDocument]
(	
	[DocumentId]		UniqueIdentifier Not Null,
	[TemplateId]		UniqueIdentifier Not Null,
	[SchemaId]			UniqueIdentifier Not Null, 
	[ObjectId]			UniqueIdentifier Null, -- Null = Fixed content
	[FileName]			[AppGeneral].[uddtFileName] Not Null,
	-- Temporal History Support
	[SysStart]			DateTime2 (7) Not Null,
	[SysEnd]			DateTime2 (7) Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_SchemaDocument]
    ON [HsScript].[SchemaDocument]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_SchemaDocument]
    ON [HsScript].[SchemaDocument]([DocumentId] ASC)
GO
