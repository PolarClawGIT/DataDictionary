CREATE PROCEDURE [App_DataDictionary].[procGetModelAttribute]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@SubjectAreaId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ModelAttribute.
*/

Select	M.[AttributeId],
		A.[AttributeTitle],
		M.[SubjectAreaId],
		S.[SubjectAreaTitle]
From	[App_DataDictionary].[ModelAttribute] M
		Inner Join [App_DataDictionary].[DomainAttribute] A
		On	M.[AttributeId] = A.[AttributeId]
		Left Join [App_DataDictionary].[ModelSubjectArea] S
		On	M.[SubjectAreaId] = S.[SubjectAreaId]
Where	(@ModelId is Null or @ModelId = M.[ModelId]) And
		(@AttributeId is Null or @AttributeId = M.[AttributeId]) And
		(@SubjectAreaId is Null or @SubjectAreaId = M.[SubjectAreaId])
		
GO