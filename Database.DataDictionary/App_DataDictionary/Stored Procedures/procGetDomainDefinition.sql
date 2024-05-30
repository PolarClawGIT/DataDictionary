CREATE PROCEDURE [App_DataDictionary].[procGetDomainDefinition]
		@DefinitionId UniqueIdentifier = Null,
		@DefinitionTitle NVarChar(100) = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainDefinition.
*/
Select	[DefinitionId],
		[DefinitionTitle],
		[DefinitionDescription]
From	[App_DataDictionary].[DomainDefinition]
Where	(@DefinitionId is Null Or @DefinitionId = [DefinitionId]) And
		(@DefinitionTitle is Null Or @DefinitionTitle = [DefinitionTitle])
GO
