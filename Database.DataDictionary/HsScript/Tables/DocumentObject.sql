CREATE TABLE [HsScript].[DocumentObject]
(
	[DocumentId]		UniqueIdentifier Not Null,
	[ObjectId]			UniqueIdentifier Not Null,
	-- Temporal History Support
	[SysStart]				DateTime2 (7) Not Null,
	[SysEnd]				DateTime2 (7) Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_DocumentObject]
    ON [HsScript].[DocumentObject]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_DocumentObject]
    ON [HsScript].[DocumentObject]([DocumentId] ASC, [ObjectId] ASC)
GO
