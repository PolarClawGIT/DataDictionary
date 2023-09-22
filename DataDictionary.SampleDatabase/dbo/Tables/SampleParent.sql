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