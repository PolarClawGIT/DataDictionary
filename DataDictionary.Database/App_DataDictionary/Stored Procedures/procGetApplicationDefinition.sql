CREATE PROCEDURE [App_DataDictionary].[procGetApplicationDefinition]
		@DefinitionId UniqueIdentifier = Null,
		@DefinitionTitle [App_DataDictionary].[typeTitle] = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ApplicationDefinition.
*/
Select	[DefinitionId],
		[DefinitionTitle],
		[DefinitionDescription],
		[Obsolete],
		[SysStart]
From	[App_DataDictionary].[ApplicationDefinition]
Where	(@DefinitionId is Null Or @DefinitionId = [DefinitionId]) And
		(@DefinitionTitle is Null Or @DefinitionTitle = [DefinitionTitle])
GO