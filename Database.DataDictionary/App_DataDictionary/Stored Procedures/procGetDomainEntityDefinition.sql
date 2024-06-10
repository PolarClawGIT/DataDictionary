CREATE PROCEDURE [App_DataDictionary].[procGetDomainEntityDefinition]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainEntityDefinition.
*/
Select	D.[EntityId],
		D.[DefinitionId],
		D.[DefinitionSummary],
		D.[DefinitionText]
From	[App_DataDictionary].[DomainEntityDefinition] D
		Left Join [App_DataDictionary].[ModelEntity] M
		On	D.[EntityId] = M.[EntityId]
Where	(@ModelId is Null Or @ModelId = M.[ModelId]) And
		(@EntityId is Null Or @EntityId = D.[EntityId])
GO