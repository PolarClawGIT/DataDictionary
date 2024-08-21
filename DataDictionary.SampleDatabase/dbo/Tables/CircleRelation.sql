CREATE TABLE [dbo].[CircleRelation]
(
    [CircleId]    INT            NOT NULL,
    [CircleData]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Circle] Primary Key ([CircleId]),
    CONSTRAINT [FK_Circle] FOREIGN KEY ([CircleId]) REFERENCES [dbo].[CircleRelation] ([CircleId]),
)
