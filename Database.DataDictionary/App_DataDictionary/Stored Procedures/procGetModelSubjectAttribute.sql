CREATE PROCEDURE [App_DataDictionary].[procGetModelSubjectAttribute]
		@ModelId UniqueIdentifier = Null,
		@SubjectAreaId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null
AS
Select	[SubjectAreaId],
		[AttributeId]
From	[App_DataDictionary].[ModelSubjectAttribute] D
Where	(@ModelId is Null Or @ModelId = D.[ModelId]) And
		(@SubjectAreaId is Null Or @SubjectAreaId = D.[SubjectAreaId]) And
		(@AttributeId is Null Or @AttributeId = D.[AttributeId])