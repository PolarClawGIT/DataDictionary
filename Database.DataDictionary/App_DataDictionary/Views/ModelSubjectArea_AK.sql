CREATE VIEW [App_DataDictionary].[ModelSubjectArea_AK]
WITH SCHEMABINDING AS
-- Enforces rule: SubjectAreaTitle must be unique within the Model.
Select	M.[ModelId],
		C.[SubjectAreaId],
		C.[SubjectAreaTitle]
From	[App_DataDictionary].[DomainSubjectArea] C
		Inner Join [App_DataDictionary].[ModelSubjectArea] M
		On	C.[SubjectAreaId] = M.[SubjectAreaId]
GO
CREATE UNIQUE CLUSTERED INDEX [PK_ModelSubjectArea]
    ON [App_DataDictionary].[ModelSubjectArea_AK]([ModelId] ASC, [SubjectAreaTitle] ASC)
GO