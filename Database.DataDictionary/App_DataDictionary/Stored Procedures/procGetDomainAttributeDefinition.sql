CREATE PROCEDURE [App_DataDictionary].[procGetDomainAttributeDefinition]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainAttributeDefinition.
*/
Select	D.[AttributeId],
		D.[DefinitionId],
		D.[DefinitionSummary],
		D.[DefinitionText]
From	[App_DataDictionary].[DomainAttributeDefinition] D
		Left Join [App_DataDictionary].[ModelAttribute] M
		On	D.[AttributeId] = M.[AttributeId]
Where	(@ModelId is Null Or @ModelId = M.[ModelId]) And
		(@AttributeId is Null Or @AttributeId = D.[AttributeId])
GO