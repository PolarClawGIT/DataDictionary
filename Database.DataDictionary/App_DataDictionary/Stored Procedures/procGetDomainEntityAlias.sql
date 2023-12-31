﻿CREATE PROCEDURE [App_DataDictionary].[procGetDomainEntityAlias]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainEntityAlias.
*/
Select	D.[EntityId],
		A.[AliasName],
		S.[ScopeName]
From	[App_DataDictionary].[DomainEntityAlias] D
		Left Join [App_DataDictionary].[ModelEntity] M
		On	D.[EntityId] = M.[EntityId]
		Outer Apply [App_DataDictionary].[funcGetAliasName](D.[AliasId]) A
		Outer Apply [App_DataDictionary].[funcGetScopeName](D.[ScopeId]) S
Where	(@ModelId is Null or @ModelId = M.[ModelId]) And
		(@EntityId is Null or @EntityId = D.[EntityId])
GO