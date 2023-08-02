CREATE PROCEDURE [App_DataDictionary].[procGetApplicationPropertyScope]
		@PropertyId UniqueIdentifier = Null,
		@ScopeType SysName = Null,
		@ObjectType SysName = Null,
		@ElementType SysName = Null
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on ApplicationPropertyScope.
*/
Select	[PropertyId],
		[ScopeType],
		[ObjectType],
		[ElementType]
From	[App_DataDictionary].[ApplicationPropertyScope]
Where	(@PropertyId is Null Or @PropertyId = [PropertyId]) And
		(@ScopeType is Null Or @ScopeType = IsNull([ScopeType], @ScopeType)) And
		(@ObjectType is Null Or @ObjectType = IsNull([ObjectType], @ObjectType)) And
		(@ElementType is Null Or @ElementType = IsNull([ElementType], @ElementType))
GO
