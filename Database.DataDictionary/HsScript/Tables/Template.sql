CREATE TABLE [HsScript].[Template]
(
	[TemplateId]            UNIQUEIDENTIFIER Not Null,
	[TemplateTitle]			[AppGeneral].[uddtTitle] Not Null,
	[TemplateDescription]	[AppGeneral].[uddtDescription] Null,
	[SysStart]				DateTime2 (7) Not Null,
	[SysEnd]				DateTime2 (7)  Not Null,
)
GO
CREATE CLUSTERED INDEX [IX_Template]
    ON [HsScript].[Template]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_Template]
    ON [HsScript].[Template]([TemplateId] ASC)
GO