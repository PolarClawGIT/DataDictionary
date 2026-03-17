CREATE TABLE [HsScript].[DocumentObject]
(	[ObjectId]			UniqueIdentifier Not Null,
	[TemplateId]		UniqueIdentifier Not Null,
	[ParentObjectId]	UniqueIdentifier NULL,
	[ObjectScope]		[AppGeneral].[uddtScopeName] Null,
	[ObjectMember]		[AppGeneral].[uddtMember] Not Null,
	[IsExcluded]		Bit Not Null,
	[KeepOrphaned]		Bit Not NULL,
	-- Temporal History Support
	[SysStart]			DateTime2 (7) Not Null,
	[SysEnd]			DateTime2 (7) Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_DocumentObject]
    ON [HsScript].[DocumentObject]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_DocumentObject]
    ON [HsScript].[DocumentObject]([ObjectId] ASC)
GO