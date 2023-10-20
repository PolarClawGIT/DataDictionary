CREATE PROCEDURE [App_DataDictionary].[procGetDomainAttributeAlias]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@DatabaseName SysName = Null,
		@SchemaName SysName = Null,
		@ObjectName SysName = Null,
		@ElementName SysName = Null

As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainAttributeAlias.
*/

Select	D.[AttributeId],
		D.[AttributeAliasId],
		D.[DatabaseName],
		D.[SchemaName],
		D.[ObjectName],
		D.[ElementName],
		D.[SysStart]
From	[App_DataDictionary].[DomainAttributeAlias] D
		Left Join [App_DataDictionary].[ModelAttribute] A
			On	D.[AttributeId] = A.[AttributeId] And
				D.[ModelId] = A.[ModelId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@AttributeId is Null or @AttributeId = D.[AttributeId]) And
		(@DatabaseName is Null or @DatabaseName = D.[DatabaseName]) And
		(@SchemaName is Null or @SchemaName = D.[SchemaName]) And
		(@ObjectName is Null or @ObjectName = D.[ObjectName]) And
		(@ElementName is Null or @ElementName = D.[ElementName])
GO