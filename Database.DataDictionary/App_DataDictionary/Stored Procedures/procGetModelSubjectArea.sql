CREATE PROCEDURE [App_DataDictionary].[procGetModelSubjectArea]
		@ModelId UniqueIdentifier = Null,
		@SubjectAreaId UniqueIdentifier = Null,
		@SubjectAreaTitle NVarChar(100) = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ModelSubjectArea.
*/

Select	S.[SubjectAreaId],
		--[SubjectAreaParentId],
		S.[SubjectAreaTitle],
		S.[SubjectAreaDescription],
		X.[SubjectAreaNameSpace]
From	[App_DataDictionary].[ModelSubjectArea] S
		Cross Apply [App_DataDictionary].[funcGetSubjectAreaNameSpace](S.[SubjectAreaId]) X
Where	(@ModelId is Null or @ModelId = S.[ModelId]) And
		(@SubjectAreaId is Null or @SubjectAreaId = S.[SubjectAreaId]) And
		(@SubjectAreaTitle is Null or @SubjectAreaTitle = S.[SubjectAreaTitle])
GO
