CREATE TABLE [dbo].[SampleShared]
(
    [SampleSharedId] INT NOT NULL,
    [SampleSharedData]     XML NOT NULL,
    CONSTRAINT [PK_SampleShared] PRIMARY KEY CLUSTERED ([SampleSharedId] ASC),
    CONSTRAINT [FK_SampleShared_SampleParent] FOREIGN KEY ([SampleSharedId]) REFERENCES [dbo].[SampleParent] ([SampleParentId])
)
