CREATE PROCEDURE [AppModel].[procGetEntity]
		@ModelId UniqueIdentifier = Null,
		@EntityId UniqueIdentifier = Null,
		@AsOfUtcDate DateTime2 (7) = Null, -- As of this UTC Date (account for timezone offset). Default is now.
		@IncludeHistory Bit = 0 -- History is included
As
Set NoCount On -- Do not show record counts
Set XACT_ABORT On -- Error severity of 11 and above causes XAct_State() = -1 and a rollback must be issued
/* Description: Performs Get on Model Entity.
*/
Set	@AsOfUtcDate = IsNull(@AsOfUtcDate, SysUtcDateTime())

;With [Dates] As (
	Select	[EntityId],
			[SysStart]
	From	[AppModel].[EntityHs] For System_Time All
	Union
	Select	[EntityId],
			[SysStart]
	From	[AppModel].[EntityAliasHs] For System_Time All
	Union
	Select	[EntityId],
			[SysStart]
	From	[AppModel].[EntityDefinitionHs] For System_Time All
	Union
	Select	[EntityId],
			[SysStart]
	From	[AppModel].[EntityPropertyHs] For System_Time All
	Union
	Select	[EntityId],
			[SysStart]
	From	[AppModel].[EntitySubjectAreaHs] For System_Time All
	Union
	Select	[EntityId],
			[SysStart]
	From	[AppModel].[EntityAttributeHs] For System_Time All)
Select	D.[EntityId],
		D.[EntityTitle],
		D.[EntityDescription],
		D.[EntityName],
		-- Temporal Data
		D.[CreatedOn],
		D.[CreatedBy],
		D.[RemovedOn],
		D.[RemovedBy],
		Convert(Bit, IIF(D.[IsInserted] = 1 And T.[SysStart] = D.[SysStart], 1,0)) As [IsInserted],
		Convert(Bit, IIF(D.[IsUpdated] = 1 Or T.[SysStart] <> D.[SysStart], 1,0)) As [IsUpdated],
		Convert(Bit, IIF(D.[IsDeleted] = 1 And T.[SysStart] = D.[SysStart], 1,0)) As [IsDeleted],
		D.[IsCurrent]
From	[Dates] T
		Inner Join [AppModel].[EntityHs] For System_Time All D
		On	T.[EntityId] = D.[EntityId] And
			T.[SysStart] >= D.[SysStart] And
			T.[SysStart] < D.[SysEnd]
Where	(@IncludeHistory = 1 Or (D.[SysStart] <= @AsOfUtcDate And D.[SysEnd] > @AsOfUtcDate)) And
		(@EntityId is Null Or @EntityId = D.[EntityId]) And
		(@ModelId is Null Or @ModelId In (
			Select	[ModelId]
			From	[AppModel].[ModelEntity] For System_Time As of @AsOfUtcDate
			Where	D.[EntityId] = [EntityId]))
GO
