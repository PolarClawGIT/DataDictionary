CREATE TABLE [dbo].[SampleChild] (
    [SampleParentId] INT NOT NULL,
    [SampleChildId]  INT NOT NULL,
    [SampleData]     XML NOT NULL,
    CONSTRAINT [PK_SampleChild] PRIMARY KEY CLUSTERED ([SampleParentId] ASC, [SampleChildId] ASC),
    CONSTRAINT [FK_SampleChild_SampleParent] FOREIGN KEY ([SampleParentId]) REFERENCES [dbo].[SampleParent] ([SampleParentId])
);
