CREATE PROCEDURE [AppModel].[procGetEntity]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainEntity.
*/

Select	D.[EntityId],
		D.[EntityTitle],
		D.[EntityDescription],
		A.[MemberName]
From	[AppModel].[Entity] D
		Left Join [AppModel].[ModelEntity] A
		On	D.[EntityId] = A.[EntityId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@EntityId is Null or @EntityId = D.[EntityId])
GO
