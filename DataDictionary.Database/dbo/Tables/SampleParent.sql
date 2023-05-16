CREATE TABLE [dbo].[SampleParent] (
    [SampleParentId]    INT            NOT NULL,
    [SampleParentTitle] NVARCHAR (100) NOT NULL,
    [SampleParentData]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_SampleParent] PRIMARY KEY CLUSTERED ([SampleParentId] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_SampleParent]
    ON [dbo].[SampleParent]([SampleParentTitle] ASC);
GO
