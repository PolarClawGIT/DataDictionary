CREATE TABLE [HsScript].[SchemaNode]
(	[NodeId]				UniqueIdentifier Not Null,
	[SchemaId]				UniqueIdentifier Not Null,
	[NodeName]				[AppGeneral].[uddtMember] Null,
	[NodeOrder]				Int Not Null,
	[RenderValueAs]			NVarChar(20) Not Null,
	[FixedValue]			NVarChar(250) Null,
	[ObjectScope]			[AppGeneral].[uddtScopeName] Null,
	[ObjectProperty]		[AppGeneral].[uddtQualifiedName] Null,
	[ModelPropertyId]		UniqueIdentifier NULL,
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
