CREATE PROCEDURE [App_DataDictionary].[procGetDomainAttributeAlias]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainAttributeAlias.
*/
Select	D.[AttributeId],
		N.[NameSpace] As [AliasName],
		D.[AliasScope]
From	[App_DataDictionary].[DomainAttributeAlias] D
		Cross Apply [App_DataDictionary].[funcGetNameSpace](D.[NameSpaceId]) N
		Left Join [App_DataDictionary].[ModelAttribute] M
		On	D.[AttributeId] = M.[AttributeId]
		Left Join [App_DataDictionary].[ModelNameSpace] S
		On	D.[NameSpaceId] = S.[NameSpaceId] And
			M.[ModelId] = S.[ModelId]
Where	(@ModelId is Null or (@ModelId = M.[ModelId] And @ModelId = S.[ModelId])) And -- NameSpace must also be for the Model Specified
		(@AttributeId is Null or @AttributeId = D.[AttributeId])
GO
