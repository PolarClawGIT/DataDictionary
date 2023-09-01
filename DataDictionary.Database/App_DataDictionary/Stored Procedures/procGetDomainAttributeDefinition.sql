CREATE PROCEDURE [App_DataDictionary].[procGetDomainAttributeDefinition]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@DefinitionId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainAttributeDefinition.
*/
Select	D.[AttributeId],
		D.[DefinitionId],
		D.[DefinitionText],
		D.[SysStart]
From	[App_DataDictionary].[DomainAttributeDefinition] D
		Inner Join [App_DataDictionary].[ModelAttribute] A
		On	D.[AttributeId] = A.[AttributeId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@AttributeId is Null or @AttributeId = D.[AttributeId]) And
		(@DefinitionId is Null Or @DefinitionId = [DefinitionId])
GO