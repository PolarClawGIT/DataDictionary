CREATE TABLE [dbo].[SampleDouble]
(
    [SampleParentId]       INT NOT NULL,
	[SampleAliasId]        INT NOT Null,
	[SampleDoubleData]     XML NOT NULL,
    CONSTRAINT [PK_SampleDouble] PRIMARY KEY CLUSTERED ([SampleParentId] ASC, [SampleAliasId] ASC),
	CONSTRAINT [FK_SampleDouble_SampleParent] FOREIGN KEY ([SampleParentId]) REFERENCES [dbo].[SampleParent] ([SampleParentId]),
	CONSTRAINT [FK_SampleDoubleAlt_SampleParent] FOREIGN KEY ([SampleAliasId]) REFERENCES [dbo].[SampleParent] ([SampleParentId])
)
