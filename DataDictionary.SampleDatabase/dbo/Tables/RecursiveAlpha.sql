CREATE TABLE [dbo].[RecursiveAlpha]
(
    [AlphaId]    INT            NOT NULL,
    [AlphaData]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_RecursiveAlpha] Primary Key ([AlphaId]),
    CONSTRAINT [FK_RecursiveAlpha] FOREIGN KEY ([AlphaId]) REFERENCES [dbo].[RecursiveBeta] ([BetaId]),
    CONSTRAINT [FK_RecursiveTest] FOREIGN KEY ([AlphaId]) REFERENCES [dbo].[RecursiveAlpha] ([AlphaId]),
)
