CREATE TABLE [HsGeneral].[HelpSubject] (
    [HelpId]      UNIQUEIDENTIFIER NOT NULL,
    [HelpSubject] [App_DataDictionary].[typeTitle] NOT NULL,
    [HelpToolTip] [App_DataDictionary].[typeDescription] NULL,
    [HelpText]    NVARCHAR (MAX)   NOT NULL,
    [NameSpace]   NVARCHAR (1023)  NULL,
    [ModifiedBy]  SysName          NOT NULL,
    [SysStart]    DATETIME2 (7)    NOT NULL,
    [SysEnd]      DATETIME2 (7)    NOT NULL
);
GO
CREATE CLUSTERED INDEX [IX_HelpSubject]
    ON [HsGeneral].[HelpSubject]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [FK_HelpSubject]
    ON [HsGeneral].[HelpSubject]([HelpId] ASC)
GO