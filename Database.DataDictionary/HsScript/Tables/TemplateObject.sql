CREATE TABLE [HsScript].[TemplateObject]
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
CREATE CLUSTERED INDEX [IX_TemplateObject]
    ON [HsScript].[TemplateObject]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_TemplateObject]
    ON [HsScript].[TemplateObject]([ObjectId] ASC)
GO