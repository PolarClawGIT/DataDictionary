CREATE PROCEDURE [App_DataDictionary].[procGetDomainAttributeAlias]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainAttributeAlias.
*/
;With [Data] As (
	Select	D.[AttributeId],
			A.[AliasId],
			D.[ScopeId],
			A.[AliasParentId],
			Convert(NVarChar(Max),FormatMessage('[%s]',[AliasElementName])) As [AliasName]
	From	[App_DataDictionary].[DomainAttributeAlias] D
			Inner Join [App_DataDictionary].[DomainAlias] A
			On	D.[AliasId] = A.[AliasId]
			Left Join [App_DataDictionary].[ModelAttribute] A
			On	D.[AttributeId] = A.[AttributeId]
	Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
			(@AttributeId is Null or @AttributeId = D.[AttributeId])
	Union All
	Select	D.[AttributeId],
			D.[AliasId],
			D.[ScopeId],
			A.[AliasParentId],
			Convert(NVarChar(Max),FormatMessage('[%s].%s',A.[AliasElementName],N.[AliasName])) As [AliasName]
	From	[Data] D
			Inner Join [App_DataDictionary].[DomainAlias] A
			On	D.[AliasParentId] = A.[AliasId])
Select	D.[AttributeId],
		D.[AliasName],
		S.[ScopeName]
From	[Data] D
		Left Join [App_DataDictionary].[ModelScope] S
		On	D.[ScopeId] = S.[ScopeId]
Where	D.[AliasParentId] is Null
GO