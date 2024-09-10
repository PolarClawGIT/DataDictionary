CREATE TABLE [Hs_General].[ApplicationHelp] (
    [HelpId]      UNIQUEIDENTIFIER NOT NULL,
    [HelpSubject] NVARCHAR (100)   NOT NULL,
    [HelpToolTip] NVARCHAR (500)   NULL,
    [HelpText]    NVARCHAR (MAX)   NOT NULL,
    [NameSpace]   NVARCHAR (1023)  NULL,
    [ModifiedBy]  SysName          NOT NULL,
    [SysStart]    DATETIME2 (7)    NOT NULL,
    [SysEnd]      DATETIME2 (7)    NOT NULL
);
GO
CREATE CLUSTERED INDEX [ix_ApplicationHelp]
    ON [Hs_General].[ApplicationHelp]([SysEnd] ASC, [SysStart] ASC)
GO
CREATE INDEX [fk_ApplicationHelp]
    ON [Hs_General].[ApplicationHelp]([HelpId] ASC)
GO