CREATE PROCEDURE [App_DataDictionary].[procGetDomainAttributeProperty]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@PropertyTitle NVarChar(100) = Null

As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainAttributeProperty.
*/

Select	D.[AttributeId],
		D.[PropertyId],
		D.[PropertyValue],
		D.[SysStart]
From	[App_DataDictionary].[DomainAttributeProperty] D
		Inner Join [App_DataDictionary].[ModelAttribute] A
		On	D.[AttributeId] = A.[AttributeId]
		Left Join [App_DataDictionary].[ApplicationProperty] P
		On	D.[PropertyId] = P.[PropertyId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@AttributeId is Null or @AttributeId = D.[AttributeId]) And
		(@PropertyTitle is Null or @PropertyTitle = P.[PropertyTitle])
GO