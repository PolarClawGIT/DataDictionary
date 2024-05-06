CREATE PROCEDURE [App_DataDictionary].[procGetDomainAttributeSubjectArea]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@SubjectAreaId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainAttributeAlias.
*/
Select	D.[AttributeId],
		D.[SubjectAreaId]
From	[App_DataDictionary].[ModelAttribute] D
		Inner Join [App_DataDictionary].[ModelSubjectArea] S
		On	D.[SubjectAreaId] = S.[SubjectAreaId]
		Cross Apply [App_DataDictionary].[funcGetNameSpace](S.[NameSpaceId]) N
Where	(@ModelId is Null or @ModelId = D.[ModelId]) And
		(@AttributeId is Null or @AttributeId = D.[AttributeId]) And
		(@SubjectAreaId is Null or @SubjectAreaId = D.[SubjectAreaId])
GO