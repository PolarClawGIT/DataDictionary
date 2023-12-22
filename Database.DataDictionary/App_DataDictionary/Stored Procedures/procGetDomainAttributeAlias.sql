CREATE PROCEDURE [App_DataDictionary].[procGetDomainAttributeAlias]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainAttributeAlias.
*/
Select	D.[AttributeId],
		A.[AliasName],
		S.[ScopeName]
From	[App_DataDictionary].[DomainAttributeAlias] D
		Left Join [App_DataDictionary].[ModelAttribute] M
		On	D.[AttributeId] = M.[AttributeId]
		Outer Apply [App_DataDictionary].[funcGetAliasName](D.[AliasId]) A
		Outer Apply [App_DataDictionary].[funcGetScopeName](D.[ScopeId]) S
Where	(@ModelId is Null or @ModelId = M.[ModelId]) And
		(@AttributeId is Null or @AttributeId = D.[AttributeId])
GO