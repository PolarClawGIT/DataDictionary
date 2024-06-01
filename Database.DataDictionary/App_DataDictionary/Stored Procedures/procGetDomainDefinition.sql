CREATE PROCEDURE [App_DataDictionary].[procGetDomainDefinition]
		@ModelId UniqueIdentifier = Null,
		@DefinitionId UniqueIdentifier = Null,
		@DefinitionTitle NVarChar(100) = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainDefinition.
*/
Select	D.[DefinitionId],
		D.[DefinitionTitle],
		D.[DefinitionDescription]
From	[App_DataDictionary].[DomainDefinition] D
		Left Join [App_DataDictionary].[ModelDefinition] M
		On	D.[DefinitionId] = M.[DefinitionId]
Where	(@ModelId is Null Or @ModelId = M.[ModelId] Or D.[IsCommon] = 1) And
		(@DefinitionId is Null Or @DefinitionId = D.[DefinitionId]) And
		(@DefinitionTitle is Null Or @DefinitionTitle = D.[DefinitionTitle])
GO
