CREATE TABLE [dbo].[RecursiveBeta]
(
    [BetaId]    INT            NOT NULL,
    [BetaData]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_RecursiveBeta] Primary Key ([BetaId]),
    CONSTRAINT [FK_RecursiveBeta] FOREIGN KEY ([BetaId]) REFERENCES [dbo].[RecursiveAlpha] ([AlphaId])
)
