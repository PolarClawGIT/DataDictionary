CREATE PROCEDURE [App_DataDictionary].[procGetDomainEntityProperty]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@PropertyId UniqueIdentifier = Null

As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainEntityProperty.
*/

Select	D.[EntityId],
		D.[PropertyId],
		D.[PropertyValue],
		D.[DefinitionText],
		D.[SysStart]
From	[App_DataDictionary].[DomainEntityProperty] D
		Inner Join [App_DataDictionary].[ModelEntity] A
		On	D.[EntityId] = A.[EntityId]
		Left Join [App_DataDictionary].[ApplicationProperty] P
		On	D.[PropertyId] = P.[PropertyId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@EntityId is Null or @EntityId = D.[EntityId]) And
		(@PropertyId is Null or @PropertyId = D.[PropertyId])
GO