CREATE TABLE [dbo].[SampleParent] (
    [SampleParentId]    INT            NOT NULL,
    [SampleSequence]    BigInt         Null CONSTRAINT [DF_SampleParent_SampleSequence] DEFAULT NEXT VALUE FOR [dbo].[SampleSequence],
    [SampleParentTitle] NVARCHAR (100) NOT NULL,
    [SampleParentData]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_SampleParent] PRIMARY KEY CLUSTERED ([SampleParentId] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_SampleParent]
    ON [dbo].[SampleParent]([SampleParentTitle] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_SampleParent]
    ON [dbo].[SampleParent]([SampleSequence] ASC);
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Primary Key Column of the Sample Parent Table',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SampleParent',
    @level2type = N'COLUMN',
    @level2name = N'SampleParentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Sample Parent Table Description',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SampleParent',
    @level2type = NULL,
    @level2name = NULL