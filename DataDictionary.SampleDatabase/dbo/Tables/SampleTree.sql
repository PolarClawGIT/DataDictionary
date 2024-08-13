CREATE TABLE [dbo].[SampleTree]
(
	[SampleTreeId]       INT NOT NULL,
	[SampleTreeParentId] INT NULL,
	[SampleTreeData]     XML NOT NULL,
	CONSTRAINT [PK_SampleTree] PRIMARY KEY CLUSTERED ([SampleTreeId] ASC),
	CONSTRAINT [FK_SampleTree_Parent] FOREIGN KEY ([SampleTreeParentId]) REFERENCES [dbo].[SampleTree] ([SampleTreeId]),
)
