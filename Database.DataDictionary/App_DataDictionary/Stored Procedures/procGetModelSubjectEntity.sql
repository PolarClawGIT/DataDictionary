CREATE PROCEDURE [App_DataDictionary].[procGetModelSubjectEntity]
		@ModelId UniqueIdentifier = Null,
		@SubjectAreaId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null
AS
Select	[SubjectAreaId],
		[EntityId]
From	[AppModel].[EntitySubjectArea] D
Where	(@ModelId is Null Or @ModelId = D.[ModelId]) And
		(@SubjectAreaId is Null Or @SubjectAreaId = D.[SubjectAreaId]) And
		(@EntityId is Null Or @EntityId = D.[EntityId])