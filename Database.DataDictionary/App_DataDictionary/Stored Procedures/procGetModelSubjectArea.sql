CREATE PROCEDURE [App_DataDictionary].[procGetModelSubjectArea]
		@ModelId UniqueIdentifier = Null,
		@SubjectAreaId UniqueIdentifier = Null,
		@SubjectAreaTitle NVarChar(100) = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ModelSubjectArea.
*/
Select	M.[SubjectAreaId],
		M.[SubjectAreaTitle],
		M.[SubjectAreaDescription],
		M.[MemberName]
From	[App_DataDictionary].[ModelSubjectArea] M
Where	(@ModelId is Null or @ModelId = M.[ModelId]) And
		(@SubjectAreaId is Null or @SubjectAreaId = M.[SubjectAreaId]) And
		(@SubjectAreaTitle is Null or @SubjectAreaTitle = M.[SubjectAreaTitle])
GO