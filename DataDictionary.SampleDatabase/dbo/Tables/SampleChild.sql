CREATE TABLE [dbo].[SampleChild] (
    [SampleParentId] INT NOT NULL,
    [SampleChildId]  INT NOT NULL,
    [SampleData]     XML NOT NULL,
    CONSTRAINT [PK_SampleChild] PRIMARY KEY CLUSTERED ([SampleParentId] ASC, [SampleChildId] ASC),
    CONSTRAINT [FK_SampleChild_SampleParent] FOREIGN KEY ([SampleParentId]) REFERENCES [dbo].[SampleParent] ([SampleParentId])
);

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Sample Child Table Description',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SampleChild',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Child Table Primary Key Refrence',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SampleChild',
    @level2type = N'COLUMN',
    @level2name = N'SampleParentId'