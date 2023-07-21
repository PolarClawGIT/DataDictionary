﻿CREATE PROCEDURE [App_DataDictionary].[procGetDomainAttributeAlias]
		@ModelId UniqueIdentifier = Null,
		@AttributeId UniqueIdentifier = Null,
		@CatalogName SysName = Null,
		@SchemaName SysName = Null,
		@ObjectName SysName = Null,
		@ElementName SysName = Null

As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainAttributeAlias.
*/

Select	A.[ModelId],
		D.[AttributeId],
		D.[AttributeAliasId],
		D.[CatalogName],
		D.[SchemaName],
		D.[ObjectName],
		D.[ElementName],
		D.[ModfiedBy],
		D.[SysStart]
From	[App_DataDictionary].[DomainAttributeAlias] D
		Inner Join [App_DataDictionary].[ApplicationAttribute] A
			On	D.[AttributeId] = A.[AttributeId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@AttributeId is Null or @AttributeId = D.[AttributeId]) And
		(@CatalogName is Null or @CatalogName = D.[CatalogName]) And
		(@SchemaName is Null or @SchemaName = D.[SchemaName]) And
		(@ObjectName is Null or @ObjectName = D.[ObjectName]) And
		(@ElementName is Null or @ElementName = D.[ElementName])
GO