CREATE TABLE [HsScript].[SchemaNodeOwner]
(	[NodeId]				UniqueIdentifier Not Null, -- Child
	[NodeOwnerId]			UniqueIdentifier Not Null, -- Parent
	[SchemaId]				UniqueIdentifier Not Null,
	-- Temporal History Support
	[SysStart]				DateTime2 (7) Not Null,
	[SysEnd]				DateTime2 (7) Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_SchemaNodeOwner]
    ON [HsScript].[SchemaNodeOwner]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_SchemaNodeOwner]
    ON [HsScript].[SchemaNodeOwner]([NodeId] ASC, [NodeOwnerId] ASC)
GO

