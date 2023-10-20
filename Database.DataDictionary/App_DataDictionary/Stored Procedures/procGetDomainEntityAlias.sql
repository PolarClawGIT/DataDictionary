CREATE PROCEDURE [App_DataDictionary].[procGetDomainEntityAlias]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@DatabaseName SysName = Null,
		@SchemaName SysName = Null,
		@ObjectName SysName = Null

As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on DomainEntityAlias.
*/

Select	D.[EntityId],
		D.[EntityAliasId],
		D.[DatabaseName],
		D.[SchemaName],
		D.[ObjectName],
		D.[SysStart]
From	[App_DataDictionary].[DomainEntityAlias] D
		Left Join [App_DataDictionary].[ModelEntity] A
			On	D.[EntityId] = A.[EntityId] And
				D.[ModelId] = A.[ModelId]
Where	(@ModelId is Null or @ModelId = A.[ModelId]) And
		(@EntityId is Null or @EntityId = D.[EntityId]) And
		(@DatabaseName is Null or @DatabaseName = D.[DatabaseName]) And
		(@SchemaName is Null or @SchemaName = D.[SchemaName]) And
		(@ObjectName is Null or @ObjectName = D.[ObjectName])
GO