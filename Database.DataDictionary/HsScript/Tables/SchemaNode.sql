CREATE TABLE [HsScript].[SchemaNode]
(	[NodeId]				UniqueIdentifier Not Null,
	[SchemaId]				UniqueIdentifier Not Null,
	[ObjectScope]			[AppGeneral].[uddtScopeName] Not Null,
	[ObjectProperty]		[AppGeneral].[uddtQualifiedName] Null,
	[NodeName]				[AppGeneral].[uddtMember] Null,
	[RenderNodeType]		NVarChar(30) Null,
	[RenderTypeCode]		NVarChar(30) Null,
	[RenderOrder]			Int Not Null,
	-- Temporal History Support
	[SysStart]				DateTime2 (7) Not Null,
	[SysEnd]				DateTime2 (7) Not Null,
)
GO

CREATE CLUSTERED INDEX [IX_SchemaNode]
    ON [HsScript].[SchemaNode]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_SchemaNode]
    ON [HsScript].[SchemaNode]([NodeId] ASC)
GO
