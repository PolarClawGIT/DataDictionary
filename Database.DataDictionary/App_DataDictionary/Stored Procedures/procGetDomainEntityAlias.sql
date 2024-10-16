﻿CREATE PROCEDURE [App_DataDictionary].[procGetDomainEntityAlias]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainEntityAlias.
*/
Select	D.[EntityId],
		N.[NameSpace] As [AliasName],
		D.[AliasScope]
From	[App_DataDictionary].[DomainEntityAlias] D
		Cross Apply [App_DataDictionary].[funcGetNameSpace](D.[NameSpaceId]) N
		Left Join [App_DataDictionary].[ModelEntity] M
		On	D.[EntityId] = M.[EntityId]
		Left Join [App_DataDictionary].[ModelNameSpace] S
		On	D.[NameSpaceId] = S.[NameSpaceId] And
			M.[ModelId] = S.[ModelId]
Where	(@ModelId is Null or (@ModelId = M.[ModelId] And @ModelId = S.[ModelId])) And -- NameSpace must also be for the Model Specified
		(@EntityId is Null or @EntityId = D.[EntityId])
GO