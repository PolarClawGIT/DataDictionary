CREATE PROCEDURE [App_DataDictionary].[procGetDomainEntitySubjectArea]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@SubjectAreaId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainEntityAlias.
*/
Select	D.[EntityId],
		D.[SubjectAreaId]
From	[App_DataDictionary].[ModelEntity] D
		Inner Join [App_DataDictionary].[ModelSubjectArea] S
		On	D.[SubjectAreaId] = S.[SubjectAreaId]
		Cross Apply [App_DataDictionary].[funcGetNameSpace](S.[NameSpaceId]) N
Where	D.[SubjectAreaId] is Not Null And
		(@ModelId is Null or @ModelId = D.[ModelId]) And
		(@EntityId is Null or @EntityId = D.[EntityId]) And
		(@SubjectAreaId is Null or @SubjectAreaId = D.[SubjectAreaId])
GO