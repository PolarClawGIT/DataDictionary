CREATE VIEW [dbo].[SampleView] AS
SELECT	P.[SampleParentId],
		P.[SampleParentTitle],
		C.[SampleData]
FROM	[dbo].[SampleParent] P
		Left Join [dbo].[SampleChild] C
		On	P.[SampleParentId] = C.[SampleParentId]
GO