CREATE PROCEDURE [App_DataDictionary].[procGetDomainAttributeAlias]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainAttributeAlias.
*/
Select	D.[AttributeId],
		N.[AliasName],
		S.[ScopeName]
From	[App_DataDictionary].[DomainAttributeAlias] D
		Inner Join [App_DataDictionary].[ModelNameSpace] N
		On	D.[AliasId] = N.[AliasId]
		Left Join [App_DataDictionary].[ModelScope] S
		On	D.[ScopeId] = S.[ScopeId]
		Left Join [App_DataDictionary].[ModelAttribute] A
		On	D.[AttributeId] = A.[AttributeId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@AttributeId is Null or @AttributeId = D.[AttributeId])
GO