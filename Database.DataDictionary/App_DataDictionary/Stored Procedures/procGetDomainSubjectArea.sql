CREATE PROCEDURE [App_DataDictionary].[procGetDomainSubjectArea]
		@ModelId UniqueIdentifier = Null,
		@SubjectAreaId UniqueIdentifier = Null,
		@SubjectAreaTitle NVarChar(100) = null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainSubjectArea.
*/
Select	D.[SubjectAreaId],
		D.[SubjectAreaTitle],
		D.[SubjectAreaDescription]
From	[App_DataDictionary].[DomainSubjectArea] D
		Left Join [App_DataDictionary].[ModelSubjectArea] A
		On	D.[SubjectAreaId] = A.[SubjectAreaId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@SubjectAreaId is Null or @SubjectAreaId = D.[SubjectAreaId]) And
		(@SubjectAreaTitle is Null or @SubjectAreaTitle = D.[SubjectAreaTitle])
GO