CREATE TABLE [dbo].[SampleShareChild]
(
    [SampleSharedChildId] INT NOT NULL,
    [SampleSharedChildData]     XML NOT NULL,
    CONSTRAINT [PK_SampleSharedChild] PRIMARY KEY CLUSTERED ([SampleSharedChildId] ASC),
    CONSTRAINT [FK_SampleShareChild_SampleShared] FOREIGN KEY ([SampleSharedChildId]) REFERENCES [dbo].[SampleShared] ([SampleSharedId])

)
