CREATE PROCEDURE [App_DataDictionary].[procGetDomainAttribute]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@AttributeTitle NVarChar(100) = null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainAttribute.
*/

Select	D.[AttributeId],
		A.[SubjectAreaId],
		D.[AttributeTitle],
		D.[AttributeDescription]
From	[App_DataDictionary].[DomainAttribute] D
		Left Join [App_DataDictionary].[ModelAttribute] A
		On	D.[AttributeId] = A.[AttributeId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@AttributeId is Null or @AttributeId = D.[AttributeId]) And
		(@AttributeTitle is Null or @AttributeTitle = D.[AttributeTitle])
GO
