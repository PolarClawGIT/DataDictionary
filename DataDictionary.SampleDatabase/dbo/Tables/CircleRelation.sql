CREATE TABLE [dbo].[CircleRelation]
(
    [CircleId]    INT            NOT NULL,
    [CircleData]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Circle] Primary Key ([CircleId]),
    CONSTRAINT [FK_Circle] FOREIGN KEY ([CircleId]) REFERENCES [dbo].[CircleRelation] ([CircleId]),
    CONSTRAINT [FK_CircleParent] FOREIGN KEY ([CircleId]) REFERENCES [dbo].[SampleParent] ([SampleParentId]),
    -- [FK_Circle] does not actually restrict anything.
    -- The FK is checked after the record is logically inserted.
    -- Because the record is present, the FK check passes.
)
