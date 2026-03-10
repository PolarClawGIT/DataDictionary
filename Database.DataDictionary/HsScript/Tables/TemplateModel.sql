CREATE TABLE [HsScript].[TemplateModel]
(
	[ModelId]		UNIQUEIDENTIFIER Not Null,
	[TemplateId]	UNIQUEIDENTIFIER Not Null,
	[SysStart]		DateTime2 (7) Not Null,
	[SysEnd]		DateTime2 (7)  Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_TemplateModel]
    ON [HsScript].[TemplateModel]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_TemplateModel]
    ON [HsScript].[TemplateModel]([ModelId] ASC, [TemplateId] ASC)
GO
