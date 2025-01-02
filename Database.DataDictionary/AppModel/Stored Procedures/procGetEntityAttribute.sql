CREATE PROCEDURE [AppModel].[procGetEntityAttribute]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Model EntityAttribute.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDateTime())

Select	[EntityId],
		[AttributeAlias],
		[AttributeName],
		[OrdinalPosition],
		[IsNullable],
		-- Temporal Data
		[CreatedOn],
		[CreatedBy],
		[RemovedOn],
		[RemovedBy],
		[IsInserted],
		[IsUpdated],
		[IsDeleted],
		[IsCurrent]
From	[AppModel].[EntityAttributeHs] For System_Time All D
Where	(@IncludeHistory = 1 Or ([SysStart] <= @AsOfUtcDate And [SysEnd] > @AsOfUtcDate)) And
		(@EntityId is Null Or @EntityId = [EntityId]) And
		(@ModelId is Null Or @ModelId In (
			Select	[ModelId]
			From	[AppModel].[ModelEntity] For System_Time As of @AsOfUtcDate
			Where	D.[EntityId] = [EntityId]))
GO
