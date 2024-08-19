CREATE TABLE [dbo].[SampleGrandChild]
(
    [SampleParentId] INT NOT NULL,
    [SampleChildId]  INT NOT NULL,
    [SampleGrandChildId]  INT NOT NULL,
    [SampleGrandChildData]     XML NOT NULL,
    CONSTRAINT [FK_SampleGrandChild_SampleChild] FOREIGN KEY ([SampleParentId], [SampleChildId]) REFERENCES [dbo].[SampleChild] ([SampleParentId], [SampleChildId])
)
