CREATE PROCEDURE [dbo].[procGetDomainAttributeProperty]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@PropertyName SysName = Null

As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainAttributeProperty.
*/

Select	A.[ModelId],
		D.[AttributeId],
		D.[PropertyName],
		D.[PropertyValue],
		D.[ModfiedBy],
		D.[SysStart]
From	[App_DataDictionary].[DomainAttributeProperty] D
		Inner Join [App_DataDictionary].[ApplicationAttribute] A
			On	D.[AttributeId] = A.[AttributeId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@AttributeId is Null or @AttributeId = D.[AttributeId]) And
		(@PropertyName is Null or @PropertyName = D.[PropertyName])
GO