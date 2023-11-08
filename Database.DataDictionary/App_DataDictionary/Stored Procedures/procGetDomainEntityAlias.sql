CREATE PROCEDURE [App_DataDictionary].[procGetDomainEntityAlias]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainEntityAlias.
*/
;With [Data] As (
	Select	D.[EntityId],
			A.[AliasId],
			D.[ScopeId],
			A.[AliasParentId],
			Convert(NVarChar(Max),FormatMessage('[%s]',[AliasElementName])) As [AliasName]
	From	[App_DataDictionary].[DomainEntityAlias] D
			Inner Join [App_DataDictionary].[DomainAlias] A
			On	D.[AliasId] = A.[AliasId]
			Left Join [App_DataDictionary].[ModelEntity] A
			On	D.[EntityId] = A.[EntityId]
	Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
			(@EntityId is Null or @EntityId = D.[EntityId])
	Union All
	Select	D.[EntityId],
			D.[AliasId],
			D.[ScopeId],
			A.[AliasParentId],
			Convert(NVarChar(Max),FormatMessage('[%s].%s',A.[AliasElementName],N.[AliasName])) As [AliasName]
	From	[Data] D
			Inner Join [App_DataDictionary].[DomainAlias] A
			On	D.[AliasParentId] = A.[AliasId])
Select	D.[EntityId],
		D.[AliasName],
		S.[ScopeName]
From	[Data] D
		Left Join [App_DataDictionary].[ModelScope] S
		On	D.[ScopeId] = S.[ScopeId]
Where	D.[AliasParentId] is Null
GO