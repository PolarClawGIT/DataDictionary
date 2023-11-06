﻿CREATE PROCEDURE [App_DataDictionary].[procGetDomainEntityAlias]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainEntityAlias.
*/
Select	D.[EntityId],
		N.[AliasName],
		S.[ScopeName]
From	[App_DataDictionary].[DomainEntityAlias] D
		Inner Join [App_DataDictionary].[ModelNameSpace] N
		On	D.[AliasId] = N.[AliasId]
		Left Join [App_DataDictionary].[ModelScope] S
		On	D.[ScopeId] = S.[ScopeId]
		Left Join [App_DataDictionary].[ModelEntity] A
		On	D.[EntityId] = A.[EntityId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@EntityId is Null or @EntityId = D.[EntityId])
GO