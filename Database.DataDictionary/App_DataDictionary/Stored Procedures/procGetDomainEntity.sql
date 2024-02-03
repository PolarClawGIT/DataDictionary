﻿CREATE PROCEDURE [App_DataDictionary].[procGetDomainEntity]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@EntityTitle NVarChar(100) = null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainEntity.
*/

Select	D.[EntityId],
		D.[EntityTitle],
		D.[EntityDescription],
		D.[TypeOfEntityId],
		P.[EntityTitle] As [TypeOfEntityTitle]
From	[App_DataDictionary].[DomainEntity] D
		Left Join [App_DataDictionary].[ModelEntity] A
		On	D.[EntityId] = A.[EntityId]
		Left Join [App_DataDictionary].[DomainEntity] P
		On	D.[TypeOfEntityId] = P.[EntityId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@EntityId is Null or @EntityId = D.[EntityId]) And
		(@EntityTitle is Null or @EntityTitle = D.[EntityTitle])
GO
